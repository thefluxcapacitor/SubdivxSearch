namespace SubdivxSearch.Models
{
    using System.Collections.Generic;

    using SubdivxSearch.Domain;

    public class SubSearchResultsModel
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string ReleaseGroup { get; set; }

        public bool IsTvShow { get; set; }

        public string Season { get; set; }

        public string Episode { get; set; }

        public string SearchTerm { get; set; }

        public IList<Sub> Subs { get; set; }
    }
}
