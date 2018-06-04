using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderLineService;
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

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Util.IoC
{
    public class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;
        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* UserProfileDao */
            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            /* ProductDao */
            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            /* ProductService */
            kernel.Bind<IProductService>().
                To<ProductService>();

            /* OrderDao */
            kernel.Bind<IOrderDao>().
                To<OrderDaoEntityFramework>();

            /* OrderLineDao */
            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();

            /* OrderLineService */
            kernel.Bind<IOrderLineService>().
                To<OrderLineService>();

            /* OrderService */
            kernel.Bind<IOrderService>().
                To<OrderService>();

            /* CategoryDao */
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            /* CategoryService */
            kernel.Bind<ICategoryService>().
                To<CategoryService>();

            /* TagDao */
            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();

            /* TagService */
            kernel.Bind<ITagService>().
                To<TagService>();

            /* CardDao */
            kernel.Bind<ICardDao>().
                To<CardDaoEntityFramework>();

            /* CardService */
            kernel.Bind<ICardService>().
                To<CardService>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["madDatabaseEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}