namespace SubdivxSearch.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SubdivxSearch.Domain;

    [TestClass]
    public class TvShowVideoParsingTests
    {
        [TestMethod]
        public void TestVideoParseCase1()
        {
            var video = new Video("Game of thrones S01E09 YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase2()
        {
            var video = new Video("Game of thrones-S01E09-YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase3()
        {
            var video = new Video("Game.of.thrones.S01E09.YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase4()
        {
            var video = new Video("Game of thrones 109 YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase5()
        {
            var video = new Video("Game of thrones-109-YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase6()
        {
            var video = new Video("Game of thrones.109.YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }
    }
}