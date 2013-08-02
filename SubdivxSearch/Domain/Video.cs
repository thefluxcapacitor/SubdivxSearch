﻿namespace SubdivxSearch.Domain
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    public class Video
    {
        private string[] knownShows;

        public string Episode { get; set; }

        public string Season { get; set; }

        public bool? TvShow { get; set; }

        public string ReleaseGroup { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public Video()
        {
        }

        public Video(string torrentName)
        {
            this.knownShows = ConfigurationManager.AppSettings["knownTvShows"].Split(new char[] { ',' });

            if (this.ParseTvShow(torrentName))
            {
                this.ParseSeasonEpisode(torrentName);
            }

            this.ParseReleaseGroup(torrentName);

            this.ParseYearAndTitle(torrentName);
        }

        private void ParseSeasonEpisode(string torrentName)
        {
            var regex = new Regex(@"[s,S][0-9]{2}[e,E][0-9]{2}");
            var result = regex.Match(torrentName);

            if (result.Success)
            {
                this.Season = torrentName.Substring(result.Index + 1, 2);
                this.Episode = torrentName.Substring(result.Index + 4, 2);
            }
            else
            {
                // This is the case of big bang theory and others (601 = season 6 episode 01)
                regex = new Regex(@"[\.,\s][0-9]{3}[\.,\s]");
                result = regex.Match(torrentName);

                if (result.Success)
                {
                    this.Season = string.Format("{0:D2}", int.Parse(torrentName.Substring(result.Index + 1, 1)));
                    this.Episode = torrentName.Substring(result.Index + 2, 2);
                }
            }

            Debug.WriteLine("Season: {0}, Episode: {1}", this.Season, this.Episode); 
        }

        private bool ParseTvShow(string torrentName)
        {
            if (!this.TvShow.HasValue)
            {
                this.TvShow = false;

                foreach (var show in this.knownShows)
                {
                    if ((torrentName.IndexOf(show, StringComparison.OrdinalIgnoreCase) > -1)
                        || (torrentName.IndexOf(show.Replace(' ', '.'), StringComparison.OrdinalIgnoreCase) > -1))
                    {
                        this.Title = show.Replace('.', ' ');
                        this.TvShow = true;
                        break;
                    }
                }

                if (this.TvShow.Value)
                {
                    Debug.WriteLine("Video evaluated as a TV show: {0}", this.Title);
                }
                else
                {
                    Debug.WriteLine("Video evaluated as a movie: {0}", this.Title);
                }
            }

            return this.TvShow.Value;
        }

        private void ParseYearAndTitle(string torrentName)
        {
            var consecutiveDigits = 0;
            var lastCharIsDigit = false;

            for (var i = 0; i < torrentName.Length; i++)
            {
                if (char.IsDigit(torrentName[i]))
                {
                    if (lastCharIsDigit)
                    {
                        consecutiveDigits++;
                    }
                    else
                    {
                        consecutiveDigits = 1;
                        lastCharIsDigit = true;
                    }
                }
                else
                {
                    lastCharIsDigit = false;

                    if (consecutiveDigits == 4)
                    {
                        this.Year = int.Parse(torrentName.Substring(i - 4, 4));

                        if (string.IsNullOrEmpty(this.Title))
                        {
                            this.Title = torrentName.Substring(0, i - 5).Replace('.', ' ');
                        }

                        consecutiveDigits = 0;
                    }
                }
            }

            if (this.Year == 0 && consecutiveDigits == 4)
            {
                this.Year = int.Parse(torrentName.Substring(torrentName.Length - 4, 4));

                if (string.IsNullOrEmpty(this.Title))
                {
                    this.Title = torrentName.Substring(0, torrentName.Length - 5).Replace('.', ' ');
                }
            }

            Debug.WriteLine("Title: {0}", this.Title);
            Debug.WriteLine("Year: {0}", this.Year);
        }

        private void ParseReleaseGroup(string torrentName)
        {
            if (torrentName.Contains("-"))
            {
                var releaseGroup = string.Empty;

                for (var j = torrentName.Length - 1; j >= 0; j--)
                {
                    if (torrentName[j] == '-')
                    {
                        break;
                    }

                    releaseGroup = torrentName[j] + releaseGroup;
                }

                this.ReleaseGroup = releaseGroup;
            }

            Debug.WriteLine("ReleaseGroup: {0}", this.ReleaseGroup);
        }

        public string GetSearchString()
        {
            if (this.TvShow.Value)
            {
                return string.Format("{0} S{1}E{2}", this.Title, this.Season, this.Episode);
            }
            else
            {
                return string.Format("{0} {1}", this.Title, this.Year);
            }
        }
    }
}