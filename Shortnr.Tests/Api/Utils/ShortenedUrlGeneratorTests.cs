using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shortnr.Api.Utils;

namespace Shortnr.Tests.Api.Utils
{
    [TestClass]
    public class ShortenedUrlGeneratorTests
    {
        [TestMethod]
        public void ShouldReturn_0()
        {
            Assert.AreEqual("0", ShortenedUrlGenerator.GenerateNext(null));
        }
        [TestMethod]
        public void ShouldReturn_1()
        {
            Assert.AreEqual("1", ShortenedUrlGenerator.GenerateNext("0"));
        }
        [TestMethod]
        public void ShouldReturn_A()
        {
            Assert.AreEqual("A", ShortenedUrlGenerator.GenerateNext("9"));
        }
        [TestMethod]
        public void ShouldReturn_a()
        {
            Assert.AreEqual("a", ShortenedUrlGenerator.GenerateNext("Z"));
        }
        [TestMethod]
        public void ShouldReturn_00()
        {
            Assert.AreEqual("00", ShortenedUrlGenerator.GenerateNext("z"));
        }
        [TestMethod]
        public void ShouldReturn_10()
        {
            Assert.AreEqual("10", ShortenedUrlGenerator.GenerateNext("0z"));
        }
        [TestMethod]
        public void ShouldReturn_hYhLKAj0()
        {
            Assert.AreEqual("hYhLKAj0", ShortenedUrlGenerator.GenerateNext("hYhLKAiz"));
        }
    }
}
