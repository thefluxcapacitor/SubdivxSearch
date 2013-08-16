namespace SubdivxSearch.Domain
{
    using System.Collections.Generic;

    public interface ISubDivXManager
    {
        IList<Sub> GetCandidateSubs(Video video, bool searchInComments, Dictionary<string, string> cache);

        byte[] DownloadSub(string downloadUrl);
    }
}