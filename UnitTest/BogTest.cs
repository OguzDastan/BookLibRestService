using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLib;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        // field for class
        private Bog b;
        [TestInitialize]
        public void Arrange()
        {
            // arrange
            FakeClass f = new FakeClass("sad");
            b = new Bog();
        }

        [TestMethod]
        public void TestObject()
        {
            // act -already made instance in "TestInitialize"

            Assert.AreSame(b.GetType(), typeof(Bog));
        }

        [TestMethod]
        public void TitleTest()
        {
            // act
            b.Title = "How to eat food";

            // assert
            Assert.AreEqual("How to eat food", b.Title);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void TitleTestFailed()
        {
            // act
            b.Title = "H";

            // assert
            Assert.AreEqual("H", b.Title);
        }

        [TestMethod]
        public void PageCountTest()
        {
            // act
            b.Pagecount = 432;

            // assert
            Assert.AreEqual(432, b.Pagecount);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void PageCountTestFailed()
        {
            // act
            b.Pagecount = 1235;

            // assert
            Assert.AreEqual(1235, b.Pagecount);
        }

        [TestMethod]
        public void ISBNTester()
        {
            // act
            b.Isbn13 = "1234567891234";

            // assert
            Assert.AreEqual("1234567891234", b.Isbn13);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void ISBNTesterFailed()
        {
            // act
            b.Isbn13 = "1";

            // assert
            Assert.AreEqual("1", b.Isbn13);
        }



    }
}
