using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Test.ICategoryServiceTest
{
    /// <summary>
    /// Descripción resumida de ICategoryServiceTest
    /// </summary>
    [TestClass]
    public class ICategoryServiceTest
    {
        private static IKernel kernel;
        private static ICategoryDao categoryDao;
        private static ICategoryService categoryService;
#pragma warning disable CS0169 // El campo 'ICategoryServiceTest.transaction' nunca se usa
        TransactionScope transaction;
#pragma warning restore CS0169 // El campo 'ICategoryServiceTest.transaction' nunca se usa
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
            categoryService = kernel.Get<ICategoryService>();
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

        /// <summary>
        ///A test for GetAllCategories
        ///</summary>
        [TestMethod()]
        public void GetAllCategoriesTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Category cat1 = new Category();
                cat1.name = "Music";
                Category cat2 = new Category();
                cat2.name = "Book";

                // Add categories
                categoryDao.Create(cat1);
                categoryDao.Create(cat2);

                // Extract all categories
                List<CategoryDetails> categories = categoryService.GetAllCategories();

                // Check the data
                Assert.AreEqual(cat1.name, categories[0].name);
                Assert.AreEqual(cat2.name, categories[1].name);
            }
        }
    }
}
