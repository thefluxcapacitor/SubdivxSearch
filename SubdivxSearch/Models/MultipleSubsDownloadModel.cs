namespace SubdivxSearch.Models
{
    using System.Collections.Generic;

    public class MultipleSubsDownloadModel
    {
        public IList<SubDownloadModel> Subs { get; set; }

        public string SearchTerm { get; set; }
    }
}