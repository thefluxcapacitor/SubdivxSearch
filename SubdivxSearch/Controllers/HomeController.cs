namespace SubdivxSearch.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ionic.Zip;

    using SharpCompress.Archive;
    using SharpCompress.Archive.Rar;
    using SharpCompress.Common;

    using SubdivxSearch.Domain;
    using SubdivxSearch.Models;

    public class HomeController : Controller
    {
        public ActionResult SearchSub(string searchTerm)
        {
            var video = new Video(searchTerm);

            var dummyCache = new Dictionary<string, string>();

            IList<Sub> subs;
            if (video.Title == "test movie" && video.Year == 2011)
            {
                subs = GetSampleSubs();
            }
            else
            {
                var subDivXManager = new SubDivXManager();
                subs = subDivXManager.GetCandidateSubs(video, true, dummyCache);
            }

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

        private static IList<Sub> GetSampleSubs()
        {
            IList<Sub> subs;
            subs = new List<Sub>();
            subs.Add(
                new Sub()
                    {
                        Title = "Test movie (2011)",
                        Description = "Son los de ArgenTeam para Bruno.DVDRip.XviD-DiXi todo el crédito pra ellos",
                        Downloads = 1500,
                        SubUrl = "http://www.google.com",
                        DownloadUrl = "http://www.subdivx.com/bajar.php?id=340626&u=7",
                        Cds = 2,
                        Comments =
                            Sub.ParseComments(
                                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />" + "<link rel=\"stylesheet\" type=\"text/css\" href=\"estilo_33.css\"><div id=\"pop_upcoment\">"
                                + "Funcionan al 100% para la version de YIFY en 1080p! gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X1567413\" target=\"new\">fkmetal333</a></div>" + "</div>"
                                + "<div id=\"pop_upcoment\">" + "Gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X647759\" target=\"new\">chamil</a></div>" + "</div>" + "<div id=\"pop_upcoment\">"
                                + "Excelente...Gracias <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X470163\" target=\"new\">XtreMaster</a></div>" + "</div>")
                    });
            subs.Add(
                new Sub()
                    {
                        Title = "Test movie (2011)",
                        Description = "Son para DVDRip-MAXSPEED basados en los de argenteam para la versión Bruno.720p.BluRay.x264-REFiNED. Creo que son los mejores para existentes para esta versión",
                        Downloads = 2500,
                        SubUrl = "http://www.google.com",
                        Cds = 1,
                        DownloadUrl = "http://www.subdivx.com/bajar.php?id=340626&u=7",
                        Comments =
                            Sub.ParseComments(
                                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />" + "<link rel=\"stylesheet\" type=\"text/css\" href=\"estilo_33.css\"><div id=\"pop_upcoment\">"
                                + "Funcionan al 100% para la version de YIFY en 1080p! gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X1567413\" target=\"new\">fkmetal333</a></div>" + "</div>"
                                + "<div id=\"pop_upcoment\">" + "Gracias! DIXI<div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X647759\" target=\"new\">chamil</a></div>" + "</div>" + "<div id=\"pop_upcoment\">"
                                + "Excelente...Gracias YIFY<div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X470163\" target=\"new\">XtreMaster</a></div>" + "</div>")
                    });
            subs.Add(
                new Sub()
                    {
                        Title = "Test movie (2011)",
                        Description = "sub sin comments - multiples subs",
                        Downloads = 2500,
                        SubUrl = "http://www.google.com",
                        Cds = 2,
                        DownloadUrl = "http://www.subdivx.com/bajar.php?id=64393&u=1"
                    });
            subs.Add(
                new Sub()
                    {
                        Title = "Test movie (2011)",
                        Description = "Los de <b>aRGENTeam</b> para la versión <b>Bruno.720p.BluRay.x264-REFiNED</b>. Excelentes como siempre.",
                        Downloads = 500,
                        DownloadUrl = "http://www.subdivx.com/bajar.php?id=340626&u=7",
                        SubUrl = "http://www.google.com",
                        Cds = 1,
                        Comments =
                            Sub.ParseComments(
                                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />" + "<link rel=\"stylesheet\" type=\"text/css\" href=\"estilo_33.css\"><div id=\"pop_upcoment\">"
                                + "Funcionan al 100% para la version de DIXI en 1080p! gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X1567413\" target=\"new\">fkmetal333</a></div>" + "</div>"
                                + "<div id=\"pop_upcoment\">" + "Gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X647759\" target=\"new\">chamil</a></div>" + "</div>" + "<div id=\"pop_upcoment\">"
                                + "Excelente...Gracias <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X470163\" target=\"new\">XtreMaster</a></div>" + "</div>")
                    });
            return subs;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        private void ByteArrayToFile(string fileName, byte[] bytes)
        {
            // Open file for reading
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                // Writes a block of bytes to this stream using data from
                // a byte array.
                stream.Write(bytes, 0, bytes.Length);

                // close file stream
                stream.Close();
            }
        }

        public ActionResult DownloadSub(string url, string fileDownloadName, string subId)
        {
            url = Server.UrlDecode(url);
            fileDownloadName = Server.UrlDecode(fileDownloadName);
            
            var mgr = new SubDivXManager();
            var bytes = mgr.DownloadSub(url);

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
                throw new Exception("Ocurrió un error al intentar descargar el subtítulo.");
            }

            if (allSubsBytes.Count == 1)
            {
                return this.SingleSubActionResult(allSubsBytes.Single(), fileDownloadName);
            }

            return this.MultipleSubsActionResult(allSubsBytes, url);
        }

        private ActionResult SingleSubActionResult(KeyValuePair<string, byte[]> sub, string fileDownloadName)
        {
            fileDownloadName = sub.Key; //TODO: for now, just download with original file name
            //fileDownloadName = string.IsNullOrEmpty(subId) ?
            //    Path.ChangeExtension(fileDownloadName, Path.GetExtension(sub.Key)) :
            //    sub.Key;

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
