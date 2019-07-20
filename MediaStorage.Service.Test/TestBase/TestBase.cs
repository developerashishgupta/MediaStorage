using System;
using MediaStorage.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class TestBase
    {
        protected static UnityContainer container;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            container = new UnityContainer();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<ILibraryService, LibraryService>();
            container.RegisterType<IMaterialTypeService, MaterialTypeService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<ITagService, TagService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterInstance<ILogger>(NSubstitute.Substitute.For<ILogger>());
        }
    }
}
