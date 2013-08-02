using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SubdivxSearch.Controllers
{
    using SubdivxSearch.Domain;

    public class HomeController : Controller
    {
        public ActionResult SearchSub(string searchTerm)
        {
            var video = new Video(searchTerm);

            var dummyCache = new Dictionary<string, string>();

            var subDivXManager = new SubDivXManager();
            var subs = subDivXManager.GetCandidateSubs(video, true, dummyCache);

            return this.View(subs);
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}
