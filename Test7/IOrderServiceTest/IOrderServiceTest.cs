using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Test.IOrderServiceTest
{
    /// <summary>
    /// Descripción resumida de IOrderServiceTest
    /// </summary>
    [TestClass]
    public class IOrderServiceTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userDao;
        private static ICardDao cardDao;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static IOrderDao orderDao;
        private static IOrderLineDao orderLineDao;
        private static IUserService userService;
        private static ICardService cardService;
        private static IProductService productService;
        private static IOrderService orderService;
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
            cardDao = kernel.Get<ICardDao>();
            userDao = kernel.Get<IUserProfileDao>();
            productDao = kernel.Get<IProductDao>();
            orderDao = kernel.Get<IOrderDao>();
            orderLineDao = kernel.Get<IOrderLineDao>();
            orderService = kernel.Get<IOrderService>();
            userService = kernel.Get<IUserService>();
            cardService = kernel.Get<ICardService>();
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

        private static long RegisterUser(string loginame,string clearPassword)
        {
            UserProfileDetails user = new UserProfileDetails("name", "secondName", "name@udc.es", "ES", "es", 12345);
            return userService.RegisterUser(loginame, clearPassword, user);
        }

        private static void AddCard(long usrId)
        {
            CardDetails card = new CardDetails("11111",111,DateTime.Now,"Debit");
            cardService.AddCard(usrId, card);
        }

        // <summary>
        ///A test for GenerateOrder
        ///</summary>
        [TestMethod()]
        public void GenerateOrderTest()
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

                Order order = orderDao.Find(orderId);

                Assert.AreEqual(orderId, order.orderId);
                Assert.AreEqual(order.usrId, usrId);
                Assert.AreEqual(cardId, order.idCard);
                Assert.AreEqual(12345, order.postalAddress);
                Assert.AreEqual(shoppingCart.Count, order.OrderLines.Count);
                Assert.AreEqual(shoppingCart[0].productId, order.OrderLines.ElementAt(0).productId);
                Assert.AreEqual(shoppingCart[1].productId, order.OrderLines.ElementAt(1).productId);
            }
        }

        // <summary>
        ///A test for FindOrder
        ///</summary>
        [TestMethod()]
        public void FindOrderTest()
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

                // FinOrder
                OrderDetails orderD = orderService.FindOrder(orderId);

                Order order = orderDao.Find(orderId);

                Assert.AreEqual(orderD.OrderId, order.orderId);
                Assert.AreEqual(order.usrId, orderD.UsrId);
                Assert.AreEqual(orderD.CardNumber, cardDao.Find(order.idCard).cardNumber);
                Assert.AreEqual(orderD.PostalAddress, order.postalAddress);
            }
        }

        // <summary>
        ///A test for ViewOrdersByUser
        ///</summary>
        [TestMethod()]
        public void ViewOrdersByUserTest()
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

                // FinOrder
                List<OrderDetails> orders = orderService.ViewOrdersByUser(usrId,0,2);

                Order order = orderDao.Find(orderId);

                Assert.AreEqual(orders.ElementAt(0).OrderId, order.orderId);
                Assert.AreEqual(order.usrId, orders.ElementAt(0).UsrId);
                Assert.AreEqual(orders.ElementAt(0).CardNumber, cardDao.Find(order.idCard).cardNumber);
                Assert.AreEqual(orders.ElementAt(0).PostalAddress, order.postalAddress);
            }
        }

        // <summary>
        ///A test for GetOrdersByUser
        ///</summary>
        [TestMethod()]
        public void GetOrdersByUserTest()
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

                int ordersByUser = orderService.GetOrdersByUser(usrId);

                // Check the data
                Assert.AreEqual(1, ordersByUser);
            }
        }
    }
}
