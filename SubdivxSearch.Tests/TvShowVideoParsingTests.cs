namespace SubdivxSearch.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SubdivxSearch.Domain;

    [TestClass]
    public class TvShowVideoParsingTests
    {
        [TestMethod]
        public void TestVideoParseCase1_1()
        {
            var video = new Video("Game of thrones S01E09 YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase1_2()
        {
            var video = new Video("Game of thrones-S01E09-YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase1_3()
        {
            var video = new Video("Game.of.thrones.S01E09.YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase2_1()
        {
            var video = new Video("Game of thrones 109 YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase2_2()
        {
            var video = new Video("Game of thrones-109-YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }

        [TestMethod]
        public void TestVideoParseCase2_3()
        {
            var video = new Video("Game of thrones.109.YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }
        
        [TestMethod]
        public void TestVideoParseCase2_4()
        {
            var video = new Video("Game of thrones.110.YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("10", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }        
        
        [TestMethod]
        public void TestVideoParseCase3_1()
        {
            var video = new Video("Game of thrones.1x9.YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("09", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }
        
        [TestMethod]
        public void TestVideoParseCase3_2()
        {
            var video = new Video("Game of thrones.1x10.YIFY");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("10", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }
        
        [TestMethod]
        public void TestVideoParseCase3_2_WithAVI()
        {
            var video = new Video("Game of thrones.1x10.YIFY avi");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("10", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }
        
        [TestMethod]
        public void TestVideoParseCase3_2_WithMP4()
        {
            var video = new Video("Game of thrones.1x10.YIFY mp4");
            Assert.AreEqual("Game of thrones", video.Title);
            Assert.AreEqual(true, video.TvShow);
            Assert.AreEqual("01", video.Season);
            Assert.AreEqual("10", video.Episode);
            Assert.AreEqual("YIFY", video.ReleaseGroup);
        }
    }
}