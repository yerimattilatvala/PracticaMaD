using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;

namespace Es.Udc.DotNet.PracticaMaD.Test.IProductServiceTest
{
    /// <summary>
    /// Descripción resumida de IProductServiceTest
    /// </summary>
    [TestClass]
    public class IProductServiceTest
    {
        private static IKernel kernel;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ITagDao tagDao;
        private static IOrderDao orderDao;
        private static IProductService productService;
#pragma warning disable CS0169 // El campo 'IProductServiceTest.transaction' nunca se usa
        TransactionScope transaction;
#pragma warning restore CS0169 // El campo 'IProductServiceTest.transaction' nunca se usa
        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            categoryDao = kernel.Get<ICategoryDao>();
            productDao = kernel.Get<IProductDao>();
            orderDao = kernel.Get<IOrderDao>();
            tagDao = kernel.Get<ITagDao>();
            productService = kernel.Get<IProductService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {

        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        #endregion
    }
}
