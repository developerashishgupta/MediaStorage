using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStorage.Host
{
    using MediaStorage.Service;
    using Unity;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<ILibraryService, LibraryService>();
            container.RegisterType<IMaterialTypeService, MaterialTypeService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<ITagService, TagService>();
            container.RegisterType<IUserService, UserService>();

            IDepartmentService departmentService = container.Resolve<IDepartmentService>();
            ILibraryService libraryService = container.Resolve<ILibraryService>();
            IMaterialTypeService materialTypeService = container.Resolve<IMaterialTypeService>();
            IMenuService menuService = container.Resolve<IMenuService>();
            ITagService tagService = container.Resolve<ITagService>();
            IUserService userService = container.Resolve<IUserService>();


            var lib = libraryService.AddLibrary(new Common.ViewModels.Library.LibraryViewModel { Name = "TestLiblary" }).Result;
            var dep = departmentService.AddDepartment(new Common.ViewModels.Department.DepartmentViewModel { Name = "Test", LibraryId = lib.Id }).Result;
            var mtype = materialTypeService.AddMaterialType(new Common.ViewModels.MaterialType.MaterialTypeViewModel { Name = "TestMaterialType" }).Result;
            var menu = menuService.AddMenu(new Common.ViewModels.Menu.MenuViewModel { Name = "Test Menu", Description = "Demo" });
            var tegData = tagService.AddTag(new Common.ViewModels.Tag.TagViewModel { Name="TestTag"});
            var userData = userService.AddUser(new Common.ViewModels.User.UserPostViewModel { Username = "TestUSer" ,IsActive=true,Mail="a@b.com"});

        }

    }
}
