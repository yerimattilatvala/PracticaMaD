using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.TagService;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Transactions;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Test.ITagServiceTest
{
    /// <summary>
    /// Descripción resumida de ITagServiceTest
    /// </summary>
    [TestClass]
    public class ITagServiceTest
    {
        private static IKernel kernel;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ITagDao tagDao;
        private static ITagService tagService;
        TransactionScope transaction;
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
            tagDao = kernel.Get<ITagDao>();
            tagService = kernel.Get<ITagService>();
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

        private static long CreateTag(string tagName, int n)
        {
            Tag tag = new Tag();
            tag.timesUsed = n;
            tag.name = tagName;
            tagDao.Create(tag);
            return tag.tagId;
        }

        private static long CreateProduct(long categoryId, string name,int units,float prize)
        {
            Product p = new Product();
            p.categoryId = categoryId;
            p.name = name;
            p.registerDate = DateTime.Now;
            p.numberOfUnits = units;
            p.prize = prize;
            productDao.Create(p);
            return p.productId;
        }

        private static long CreateCategory(string categoryName)
        {
            Category cat = new Category();
            cat.name = categoryName;
            categoryDao.Create(cat);
            return cat.categoryId;
        }

        /// <summary>
        ///A test for GetAllTags
        ///</summary>
        [TestMethod()]
        public void GetAllTagsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string tagName1 = "Rock";
                string tagName2 = "Pop";

                // Add tags
                CreateTag(tagName1, 0);
                CreateTag(tagName2, 0);

                // Extract all tags
                List<TagDetails> tags = tagService.GetAllTags();

                // Check the data
                Assert.AreEqual(tagName1, tags[0].name);
                Assert.AreEqual(tagName2, tags[1].name);
            }
        }

        /// <summary>
        ///A test for GetMostPopularTags
        ///</summary>
        [TestMethod()]
        public void GetMostPopularTagsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Add tags
                for (int i = 0; i < 10; i++)
                {
                    string tagName = "tag" + i;
                    CreateTag(tagName, i);
                }

                // Extract tags
                List<TagDetails> tags = tagService.GetMostPopularTags(5);

                // Check the data
                Assert.AreEqual(5, tags.Count);
                Assert.AreEqual("tag9", tags[0].name);
                Assert.AreEqual("tag8", tags[1].name);
                Assert.AreEqual("tag7", tags[2].name);
                Assert.AreEqual("tag6", tags[3].name);
                Assert.AreEqual("tag5", tags[4].name);

            }
        }

        /// <summary>
        ///A test for GetMostPopularTags
        ///</summary>
        [TestMethod()]
        public void TagProductTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create Category
                long categoryId = CreateCategory("Music");

                // Create Product
                long productId = CreateProduct(categoryId, "ACDC", 10, (float)10.5);

                // Create Tag
                long tagId = CreateTag("Rock", 0);

                // Tag a product
                tagService.TagProduct(productId,tagId);

                // Check the data
                Tag tag = tagDao.Find(tagId);
                Assert.AreEqual(tag.timesUsed,1);
                Assert.AreEqual(tag.Products.ElementAt(0).productId , productId);
                Assert.AreEqual(productDao.Find(productId).Tags.ElementAt(0).tagId, tagId);
            }
        }

    }
}
