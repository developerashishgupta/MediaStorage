using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class LibraryServiceTest : TestBase
    {


        [TestInitialize]
        public void TestSetup()
        {
            ILibraryService libraryService = container.Resolve<ILibraryService>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
