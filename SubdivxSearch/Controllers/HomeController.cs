namespace SubdivxSearch.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using SharpCompress.Archive;
    using SharpCompress.Common;

    using SubdivxSearch.Domain;
    using SubdivxSearch.Models;

    public class HomeController : Controller
    {
        private readonly ISubDivXManager subDivXManager;

        public HomeController()
        {
            this.subDivXManager = new TestingSubDivXManager();
        }

        public ActionResult SearchSub(string searchTerm, bool feelingLucky = false)
        {
            var video = new Video(searchTerm);

            var dummyCache = new Dictionary<string, string>();

            var subs = this.subDivXManager.GetCandidateSubs(video, true, dummyCache);
            subs = subs.OrderByDescending(sub => sub.Downloads).ToList();

            if (!string.IsNullOrEmpty(video.ReleaseGroup))
            {
                var comparer = new CommentComparer(video.ReleaseGroup);
                foreach (var sub in subs)
                {
                    if (sub.Comments != null)
                    {
                        sub.Comments = sub.Comments.OrderBy(c => c, comparer).ToList();
                    }
                }
            }

            if (!feelingLucky || !subs.Any())
            {
                var model = new SubSearchResultsModel();
                model.Subs = subs;
                model.Title = video.Title;
                model.Year = video.Year != 0 ? video.Year.ToString(CultureInfo.InvariantCulture) : string.Empty;
                model.ReleaseGroup = video.ReleaseGroup;
                model.IsTvShow = video.TvShow.GetValueOrDefault();
                model.Season = video.Season;
                model.Episode = video.Episode;
                model.SearchTerm = searchTerm;

                return this.View(model);
            }
            else
            {
                var sub = subs.First();
                return this.DownloadSub(sub.DownloadUrl, searchTerm, null, true);
            }
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult DownloadSub(string url, string fileDownloadName, string subId, bool feelingLucky = false)
        {
            url = Server.UrlDecode(url);
            fileDownloadName = Server.UrlDecode(fileDownloadName);
            
            var bytes = this.subDivXManager.DownloadSub(url);

            var allSubsBytes = new Dictionary<string, byte[]>();

            using (var stream = new MemoryStream(bytes))
            {
                var archive = ArchiveFactory.Open(stream, Options.KeepStreamsOpen);

                if (!string.IsNullOrEmpty(subId))
                {
                    this.AddMatchingSub(allSubsBytes, archive, subId, fileDownloadName);
                }
                else
                {
                    this.AddAllSubs(allSubsBytes, archive);
                }
            }

            if (!allSubsBytes.Any())
            {
                // If no srt or sub found, maybe subs are compressed. Return original rar file then.
                return this.File(
                    bytes,
                    System.Net.Mime.MediaTypeNames.Application.Octet,
                    "subs.rar");
                //throw new Exception("Ocurrió un error al intentar descargar el subtítulo.");
            }

            if (allSubsBytes.Count == 1 || feelingLucky)
            {
                return this.SingleSubActionResult(allSubsBytes.First(), fileDownloadName);
            }

            return this.MultipleSubsActionResult(allSubsBytes, url);
        }

        private ActionResult SingleSubActionResult(KeyValuePair<string, byte[]> sub, string fileDownloadName)
        {
            //fileDownloadName = sub.Key; //TODO: for now, just download with original file name

            if (!string.IsNullOrWhiteSpace(fileDownloadName))
            {
                fileDownloadName = string.Format("{0}{1}", fileDownloadName, Path.GetExtension(sub.Key));
            }
            else
            {
                fileDownloadName = sub.Key; 
            }

            return this.File(
                sub.Value,
                System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName);
        }

        private ActionResult MultipleSubsActionResult(Dictionary<string, byte[]> allSubsBytes, string url)
        {
            var model = new MultipleSubsDownloadModel();
            model.Subs = new List<SubDownloadModel>();

            var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);

            foreach (var sub in allSubsBytes)
            {
                model.Subs.Add(new SubDownloadModel
                {
                    DownloadUrl = urlHelper.Action(
                        "DownloadSub", 
                        new 
                        {
                            url,
                            fileDownloadName = sub.Key,
                            subId = sub.Key.GetHashCode()
                        }),
                    FileName = sub.Key
                });
            }

            return this.View(model);
        }

        private void AddAllSubs(Dictionary<string, byte[]> allSubsBytes, IArchive archive)
        {
            foreach (var entry in archive.Entries)
            {
                if (!entry.IsDirectory && !string.IsNullOrEmpty(entry.FilePath))
                {
                    var subExtension = Path.GetExtension(entry.FilePath).ToLower();
                    var key = Path.GetFileName(entry.FilePath);

                    if (subExtension == ".srt" || subExtension == ".sub")
                    {
                        if (allSubsBytes.Any())
                        {
                            // if there are more than one sub, there's no need to extract them all
                            allSubsBytes.Add(key, new byte[0]);
                        }
                        else
                        {
                            allSubsBytes.Add(key, this.GetSubBytes(entry));
                        }
                    }
                }
            }
        }

        private void AddMatchingSub(Dictionary<string, byte[]> allSubsBytes, IArchive archive, string subId, string fileDownloadName)
        {
            var match = archive.Entries.FirstOrDefault(
                e => !e.IsDirectory &&
                    !string.IsNullOrEmpty(e.FilePath) &&
                    Path.GetFileName(e.FilePath).GetHashCode() == int.Parse(subId));

            if (match != null)
            {
                allSubsBytes.Add(fileDownloadName, this.GetSubBytes(match));
            }
        }

        private byte[] GetSubBytes(IArchiveEntry archiveEntry)
        {
            using (var subStream = new MemoryStream())
            {
                archiveEntry.WriteTo(subStream);
                return subStream.ToArray();
            }
        }
    }
}
