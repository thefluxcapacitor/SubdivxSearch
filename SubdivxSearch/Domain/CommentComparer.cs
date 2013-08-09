namespace SubdivxSearch.Domain
{
    using System;
    using System.Collections.Generic;

    public class CommentComparer : IComparer<string>
    {
        private readonly string releaseGroup;

        public CommentComparer(string releaseGroup)
        {
            this.releaseGroup = releaseGroup;
        }

        public int Compare(string x, string y)
        {
            if (x.IndexOf(this.releaseGroup, StringComparison.OrdinalIgnoreCase) > -1 && 
                y.IndexOf(this.releaseGroup, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return -1;
            } 
            
            if (y.IndexOf(this.releaseGroup, StringComparison.OrdinalIgnoreCase) > -1 &&
                x.IndexOf(this.releaseGroup, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return 1;
            }

            return 0;
        }
    }
}