namespace SubdivxSearch.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SubdivxSearch.Domain;

    [TestClass]
    public class MovieVideoParsingTests
    {
        [TestMethod]
        public void TestVideoParseCase1()
        {
            var video = new Video("Oblivion (2013) 1080p BrRip x264 - YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase2()
        {
            var video = new Video("Oblivion 2013 1080p BrRip x264 - YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase3()
        {
            var video = new Video("Oblivion 2013 1080p BrRip x264 YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase4()
        {
            var video = new Video("Oblivion (2013) 1080p BrRip x264 - YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase5()
        {
            var video = new Video("Oblivion.2013.1080p BrRip x264.YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase6()
        {
            var video = new Video("Oblivion.2013-YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase7()
        {
            var video = new Video("Oblivion[2013]1080p BrRip x264 - YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase8()
        {
            var video = new Video("Grown Ups 2 2013 READNFO TS XViD AC3-FREE");
            Assert.AreEqual("Grown Ups 2", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("FREE", video.ReleaseGroup);
        }

        //Not supported
        //[TestMethod]
        //public void TestVideoParseTitleHasFourOrMoreDigits()
        //{
        //    var video = new Video("THX1138 (1971) DVDr NTSC iso PTM");
        //    Assert.AreEqual("THX1138", video.Title);
        //    Assert.AreEqual(1971, video.Year);
        //    Assert.AreEqual("PTM", video.ReleaseGroup);
        //}

        [TestMethod]
        public void TestVideoParseOnlyTitleAndYear()
        {
            var video = new Video("Oblivion 2013");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual(string.Empty, video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseOnlyTitleWithSpacesAndYear()
        {
            var video = new Video("Back to the future 1985");
            Assert.AreEqual("Back to the future", video.Title);
            Assert.AreEqual(1985, video.Year);
            Assert.AreEqual(string.Empty, video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseReleaseGroupWithDigit()
        {
            var video = new Video("Oblivion.2013.1080p BrRip x264.LEGI0N");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("LEGI0N", video.ReleaseGroup);
        }
        
        [TestMethod]
        public void TestVideoParseOnlyWithTitleWithoutSpaces()
        {
            var video = new Video("Oblivion");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(0, video.Year);
            Assert.AreEqual(string.Empty, video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseOnlyWithTitleWithSpaces()
        {
            var video = new Video("Game of thrones");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(0, video.Year);
            Assert.AreEqual(string.Empty, video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseWithoutYear()
        {
            var video = new Video("Oblivion - DVDScr - XViD - YIFY");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(0, video.Year);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseReleaseGroupBetweenBrackets()
        {
            var video = new Video("Oblivion.2013.1080p BrRip x264 [LEGI0N]");
            Assert.AreEqual("Oblivion", video.Title);
            Assert.AreEqual(2013, video.Year);
            Assert.AreEqual("LEGI0N", video.ReleaseGroup);
        }
    }
}
