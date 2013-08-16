namespace SubdivxSearch.Domain
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class TestingSubDivXManager : ISubDivXManager
    {
        private readonly SubDivXManager wrappedManager;
        
        public TestingSubDivXManager()
        {
            this.wrappedManager = new SubDivXManager();
        }

        public IList<Sub> GetCandidateSubs(Video video, bool searchInComments, Dictionary<string, string> cache)
        {
            IList<Sub> subs;

            if (video.Title == "test movie" && video.Year == 2011)
            {
                subs = GetSampleSubs();
            }
            else
            {
                subs = this.wrappedManager.GetCandidateSubs(video, true, cache);
            }

            return subs;
        }

        private static IList<Sub> GetSampleSubs()
        {
            IList<Sub> subs = new List<Sub>();
            subs.Add(
                new Sub()
                {
                    Title = "Test movie (2011)",
                    Description = "Son los de ArgenTeam para Bruno.DVDRip.XviD-DiXi todo el crédito pra ellos",
                    Downloads = 1500,
                    SubUrl = "http://www.google.com",
                    DownloadUrl = "testsubs/singlesub.rar",
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
                    DownloadUrl = "testsubs/singlesub.rar",
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
                    DownloadUrl = "testsubs/multisubs.rar"
                });
            subs.Add(
                new Sub()
                {
                    Title = "Test movie (2011)",
                    Description = "Los de <b>aRGENTeam</b> para la versión <b>Bruno.720p.BluRay.x264-REFiNED</b>. Excelentes como siempre.",
                    Downloads = 500,
                    DownloadUrl = "testsubs/singlesub.rar",
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

        public byte[] DownloadSub(string downloadUrl)
        {
            if (downloadUrl.StartsWith("testsubs/"))
            {
                var localPath = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", string.Empty);
                var subFile = Path.Combine(Path.GetDirectoryName(localPath), @"App_Data\" + downloadUrl.Replace("testsubs/", string.Empty));
                return System.IO.File.ReadAllBytes(subFile);
            }
            
            return this.wrappedManager.DownloadSub(downloadUrl);
        }
    }
}