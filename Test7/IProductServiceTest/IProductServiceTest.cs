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
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;

namespace Es.Udc.DotNet.PracticaMaD.Test.IProductServiceTest
{
    /// <summary>
    /// Descripción resumida de IProductServiceTest
    /// </summary>
    [TestClass]
    public class IProductServiceTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userDao;
        private static ICardDao cardDao;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ITagDao tagDao;
        private static IOrderDao orderDao;
        private static IProductService productService;
<<<<<<< HEAD
#pragma warning disable CS0169 // El campo 'IProductServiceTest.transaction' nunca se usa
        TransactionScope transaction;
#pragma warning restore CS0169 // El campo 'IProductServiceTest.transaction' nunca se usa
=======
        private static ITagService tagService;
        private static IUserService userService;
        private static ICardService cardService;
        private static IOrderService orderService;
        TransactionScope transaction;
>>>>>>> master
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
            cardDao = kernel.Get<ICardDao>();
            userDao = kernel.Get<IUserProfileDao>();
            productService = kernel.Get<IProductService>();
            tagService = kernel.Get<ITagService>();
            userService = kernel.Get<IUserService>();
            cardService = kernel.Get<ICardService>();
            orderService = kernel.Get<IOrderService>();
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

        private static long CreateProduct(long categoryId, string name, int units, float prize)
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

        // <summary>
        ///A test for FindByKeywords using only keywords
        ///</summary>
        [TestMethod()]
        public void FindByKeyWordsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a category
                long categoryId = CreateCategory("Music");

                // Create a product
                long productId = CreateProduct(categoryId,"Dire Straits",10,(float)8.65);

                // Find by keywords
                List<ProductDetails> products = productService.FindByKeywords("dire",0,2);

                Product product = productDao.Find(productId);
                // Check the data
                Assert.AreEqual(productId,products[0].productId);
                Assert.AreEqual(categoryDao.Find(product.categoryId).name, products[0].category);
                Assert.AreEqual(product.name, products[0].name);
                Assert.AreEqual(product.numberOfUnits, products[0].numberOfUnits);
                Assert.AreEqual(product.prize, products[0].prize);
            }
        }

        // <summary>
        ///A test for FindByKeywords using keywords and categoryId
        ///</summary>
        [TestMethod()]
        public void FindByKeyWordsAndCategoryTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a category
                long categoryId = CreateCategory("Music");

                // Create a product
                long productId = CreateProduct(categoryId, "Dire Straits", 10, (float)8.65);

                // Find by keywords
                List<ProductDetails> products = productService.FindByKeywords("dire",categoryId, 0, 2);

                Product product = productDao.Find(productId);
                // Check the data
                Assert.AreEqual(productId, products[0].productId);
                Assert.AreEqual(categoryDao.Find(product.categoryId).name, products[0].category);
                Assert.AreEqual(product.name, products[0].name);
                Assert.AreEqual(product.numberOfUnits, products[0].numberOfUnits);
                Assert.AreEqual(product.prize, products[0].prize);
            }
        }

        // <summary>
        ///A test for FindByTag
        ///</summary>
        [TestMethod()]
        public void FindByTagTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a category
                long categoryId = CreateCategory("Music");

                // Create a product
                long productId = CreateProduct(categoryId, "Dire Straits", 10, (float)8.65);

                // Create a tag
                long tagId = CreateTag("Rock", 0);

                // Tag a product
                tagService.TagProduct(productId,tagId);

                // Find by keywords
                List<ProductDetails> products = productService.FindByTag(tagId);

                Product product = productDao.Find(productId);
                // Check the data
                Assert.AreEqual(productId, products[0].productId);
                Assert.AreEqual(categoryDao.Find(product.categoryId).name, products[0].category);
                Assert.AreEqual(product.name, products[0].name);
                Assert.AreEqual(product.numberOfUnits, products[0].numberOfUnits);
                Assert.AreEqual(product.prize, products[0].prize);
            }
        }

        // <summary>
        ///A test for FindProduct
        ///</summary>
        [TestMethod()]
        public void FindProductTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a category
                long categoryId = CreateCategory("Music");

                // Create a product
                long productId = CreateProduct(categoryId, "Dire Straits", 10, (float)8.65);

                // Find by keywords
                ProductDetails product = productService.FindProduct(productId);
                
                // Check the data
                Assert.AreEqual(productId, product.productId);
                Assert.AreEqual(categoryDao.Find(categoryId).name, product.category);
                Assert.AreEqual(product.name, product.name);
                Assert.AreEqual(10, product.numberOfUnits);
                Assert.AreEqual((float)8.65, product.prize);
            }
        }

        // <summary>
        ///A test for getNumberOfProductsByKeywords
        ///</summary>
        [TestMethod()]
        public void getNumberOfProductsByKeywordsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a category
                long categoryId = CreateCategory("Music");

                // Create a product
                CreateProduct(categoryId, "Dire Straits", 10, (float)8.65);
                CreateProduct(categoryId, "Dire Straits Live", 10, (float)8.65);

                // Find by keywords
                int n = productService.getNumberOfProductsByKeywords("dire");
                int n2 = productService.getNumberOfProductsByKeywords("dire",categoryId);
                // Check the data
                Assert.AreEqual(2, n);
                Assert.AreEqual(2, n2);
            }
        }

        // <summary>
        ///A test for GetOrderLineProductsByOrderId
        ///</summary>
        [TestMethod()]
        public void GetOrderLineProductsByOrderIdTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a category
                long categoryId = CreateCategory("Music");

                // Create a product
                long productId = CreateProduct(categoryId, "Dire Straits", 10, (float)8.65);
                long product2Id = CreateProduct(categoryId, "ACDC", 10, (float)8.65);

                // Register User
                UserProfileDetails user = new UserProfileDetails("pepito", "delospalotes", "pepito@udc.es", "es", "ES", 12345);
                long usrId = userService.RegisterUser("pepito07", "abcbd1234", user);

                // Get Products
                List<ProductDetails> shoppingCart = new List<ProductDetails>();
                ProductDetails p1 = productService.FindProduct(productId);
                p1.numberOfUnits = 1;
                ProductDetails p2 = productService.FindProduct(product2Id);
                shoppingCart.Add(p1);
                shoppingCart.Add(p2);

                // Add card
                CardDetails card = new CardDetails("123456789", 123, DateTime.Now, "Credit");
                cardService.AddCard(usrId, card);
                long cardId = userDao.Find(usrId).Cards.ElementAt(0).idCard;
                
                // Generate order
                long orderId = orderService.GenerateOrder(usrId, cardId, 12345, shoppingCart);

                // Retrieval the products
                List<ProductDetails> products = productService.GetOrderLineProductsByOrderId(orderId);

                // Check the data
                Assert.AreEqual(productId,products[0].productId);
            }
        }
    }
}
