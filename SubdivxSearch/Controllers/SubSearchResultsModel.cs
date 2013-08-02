namespace SubdivxSearch.Controllers
{
    using System.Collections.Generic;

    using SubdivxSearch.Domain;

    public class SubSearchResultsModel
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string ReleaseGroup { get; set; }

        public IList<Sub> Subs { get; set; }
    }
}
