using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
   public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            kernel.Bind<ICardDao>().
                To<CardDaoEntityFramework>();

            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();

            kernel.Bind<IOrderDao>().
                To<OrderDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().
               To<CategoryDaoEntityFramework>();

            kernel.Bind<ITagDao>().
               To<TagDaoEntityFramework>();

            kernel.Bind<ICardService>().
                To<CardService>();

            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<ITagService>().
                To<TagService>();

            kernel.Bind<IOrderService>().
                To<OrderService>();

            kernel.Bind<ICategoryService>().
                To<CategoryService>();

            kernel.Bind<IProductService>().
                To<ProductService>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["madDatabaseEntitiesTest"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }


        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }


        public static void ClearNInjectKernel(IKernel kernel)
        {

            kernel.Dispose();
        }
    }
}
