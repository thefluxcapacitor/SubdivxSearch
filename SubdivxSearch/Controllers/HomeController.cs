using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SubdivxSearch.Controllers
{
    using System.Globalization;

    using SubdivxSearch.Domain;

    public class HomeController : Controller
    {
        public ActionResult SearchSub(string searchTerm)
        {
            var video = new Video(searchTerm);

            var dummyCache = new Dictionary<string, string>();

            IList<Sub> subs;
            if (video.Title == "test movie" && video.Year == 2011)
            {
                subs = new List<Sub>();
                subs.Add(new Sub()
                {
                    Title = "Test movie (2011)",
                    Description = "Son los de ArgenTeam para Bruno.DVDRip.XviD-DiXi todo el crédito pra ellos",
                    Downloads = 1500,
                    SubUrl = "http://www.google.com",
                    DownloadUrl = "http://www.subdivx.com/bajar.php?id=176772&u=5",
                    Cds = 2,
                    Comments = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />" +
                        "<link rel=\"stylesheet\" type=\"text/css\" href=\"estilo_33.css\"><div id=\"pop_upcoment\">" +
                        "Funcionan al 100% para la version de YIFY en 1080p! gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X1567413\" target=\"new\">fkmetal333</a></div>" +
                        "</div>" +
                        "<div id=\"pop_upcoment\">" +
                        "Gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X647759\" target=\"new\">chamil</a></div>" +
                        "</div>" +
                        "<div id=\"pop_upcoment\">" +
                        "Excelente...Gracias <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X470163\" target=\"new\">XtreMaster</a></div>" +
                        "</div>"
                });
                subs.Add(new Sub() 
                { 
                    Title = "Test movie (2011)", Description = "Son para DVDRip-MAXSPEED basados en los de argenteam para la versión Bruno.720p.BluRay.x264-REFiNED. Creo que son los mejores para existentes para esta versión", Downloads = 2500, SubUrl = "http://www.google.com", Cds = 1,
                    DownloadUrl = "http://www.subdivx.com/bajar.php?id=176772&u=5",
                    Comments = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />" +
                        "<link rel=\"stylesheet\" type=\"text/css\" href=\"estilo_33.css\"><div id=\"pop_upcoment\">" +
                        "Funcionan al 100% para la version de YIFY en 1080p! gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X1567413\" target=\"new\">fkmetal333</a></div>" +
                        "</div>" +
                        "<div id=\"pop_upcoment\">" +
                        "Gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X647759\" target=\"new\">chamil</a></div>" +
                        "</div>" +
                        "<div id=\"pop_upcoment\">" +
                        "Excelente...Gracias <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X470163\" target=\"new\">XtreMaster</a></div>" +
                        "</div>"
                });
                subs.Add(new Sub()
                {
                    Title = "Test movie (2011)",
                    Description = "Los de <b>aRGENTeam</b> para la versión <b>Bruno.720p.BluRay.x264-REFiNED</b>. Excelentes como siempre.",
                    Downloads = 500,
                    DownloadUrl = "http://www.subdivx.com/bajar.php?id=176772&u=5",
                    SubUrl = "http://www.google.com",
                    Cds = 1,
                    Comments = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />" +
                        "<link rel=\"stylesheet\" type=\"text/css\" href=\"estilo_33.css\"><div id=\"pop_upcoment\">" +
                        "Funcionan al 100% para la version de YIFY en 1080p! gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X1567413\" target=\"new\">fkmetal333</a></div>" +
                        "</div>" +
                        "<div id=\"pop_upcoment\">" +
                        "Gracias! <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X647759\" target=\"new\">chamil</a></div>" +
                        "</div>" +
                        "<div id=\"pop_upcoment\">" +
                        "Excelente...Gracias <div id=\"pop_upcoment_der\"><a class=\"link1\" href=\"http://www.subdivx.com/X9X470163\" target=\"new\">XtreMaster</a></div>" +
                        "</div>"
                });
            }
            else
            {
                var subDivXManager = new SubDivXManager();
                subs = subDivXManager.GetCandidateSubs(video, true, dummyCache);
            }

            subs = subs.OrderByDescending(sub => sub.Downloads).ToList();

            var model = new SubSearchResultsModel();
            model.Subs = subs;
            model.Title = video.Title;
            model.Year = video.Year != 0 ? video.Year.ToString(CultureInfo.InvariantCulture) : string.Empty;
            model.ReleaseGroup = video.ReleaseGroup;

            return this.View(model);
        }

        public ActionResult Index()
        {
            return this.View();
        }

        //public FileResult DownloadSub(string url, string fileName)
        //{
        //    var mgr = new SubDivXManager();
        //    var bytes = mgr.DownloadSub(url);
        //    return this.File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "test.srt");
        //}
    }
}
