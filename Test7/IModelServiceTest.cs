using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.Util;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de IModelServiceTest
    /// </summary>
    [TestClass]
    public class IModelServiceTest
    {
        private static IKernel kernel;
        private static IModelService modelService;
        private static IUserProfileDao userProfileDao;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;
        private static ICardDao cardDao;
        private static IOrderDao orderDao;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const int address = 12345;
        private const long NON_EXISTENT_USER_ID = -1;

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

        private long CreateCategory(string categoryName)
        {
            Category category = new Category();
            category.name = categoryName;
            categoryDao.Create(category);

            return category.categoryId;
        }

        private long CreateProduct(long categoryId, string name, DateTime registerDate, int numberOfUnits, float prize)
        {
            Product product = new Product();
            product.categoryId = categoryId;
            product.name = name;
            product.registerDate = registerDate;
            product.numberOfUnits = numberOfUnits;
            product.prize = prize;

            productDao.Create(product);

            return product.productId;
        }

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            cardDao = kernel.Get<ICardDao>();
            productDao = kernel.Get<IProductDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            modelService = kernel.Get<IModelService>();

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
        ///A test for GenerateOrder
        ///</summary>
        [TestMethod()]
        public void GenerateOrderTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a categories

                long categoryId1 = CreateCategory("Music");
                long categoryId2 = CreateCategory("Books");

                // Create a product
                string name1 = "ACDC";
                DateTime registerDate1 = DateTime.Now;
                int capacity1 = 5;
                float prize1 = 13;
                long productId1 = CreateProduct(categoryId1, name1, registerDate1, capacity1, prize1);

                /*string name2 = "Codigo da Vinci";
                DateTime registerDate2 = DateTime.Now;
                int capacity2 = 5;
                float prize2 = 7;
                long productId2 = CreateProduct(categoryId2, name2, registerDate2, capacity2, prize2);
                */
                ProductDetails p1 = new ProductDetails(productId1, 2);
                //ProductDetails p2 = new ProductDetails(productId2, 1);

                List<ProductDetails> carrito = new List<ProductDetails>();
                carrito.Add(p1);
                //carrito.Add(p2);
                // Register user and find profile
                long userId =
                    modelService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, address));

                // Add a card
                int cardNumber = 11111;
                int verficationCode = 222;
                DateTime expirationDate = DateTime.Now;
                string type = "Credit";
                CardDetails cardDetails = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                modelService.AddCard(userId, cardDetails);

                // Generate a order
                OrderDetails order = modelService.GenerateOrder(userId, cardNumber,address, carrito);

                Order orderM = orderDao.Find(order.OrderId);
                // Check the data
                Assert.AreEqual(order.OrderId, orderM.orderId);
                Assert.AreEqual(order.OrderDate, orderM.orderDate);
                Assert.AreEqual(order.UsrId, orderM.usrId);
                Assert.AreEqual(order.CardNumber, orderM.cardNumber);
                Assert.AreEqual(order.PostalAddress,orderM.postalAddress);
                //transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for FindByKeyWords
        ///</summary>
        [TestMethod()]
        public void FindByKeyWordsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Create a categories

                long categoryId1 = CreateCategory("Music");

                // Create a product
                string name1 = "ACDC";
                DateTime registerDate1 = DateTime.Now;
                int capacity1 = 5;
                float prize1 = 13;
                long productId1 = CreateProduct(categoryId1,name1,registerDate1,capacity1,prize1);
                
                // Fin product by keywords
                List<ProductDetails> products = modelService.FindByKeywords(name1);

                // Check the data
                Assert.AreEqual(name1, products[0].name);
                Assert.AreEqual(registerDate1, products[0].registerDate);
                Assert.AreEqual(prize1, products[0].prize);
                Assert.AreEqual(categoryId1, products[0].category);
                //transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for AddCard
        ///</summary>
        [TestMethod()]
        public void AddCardTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and find profile
                long userId =
                    modelService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, address));

                UserProfile userProfile = userProfileDao.Find(userId);

                // Add a card and find
                int cardNumber = 11111;
                int verficationCode = 222;
                DateTime expirationDate = DateTime.Now;
                string type = "Credit";
                CardDetails cardDetails = new CardDetails(cardNumber,verficationCode,expirationDate,type);
                modelService.AddCard(userId, cardDetails);

                Card card = cardDao.Find(cardNumber);
                // Check data
                Assert.AreEqual(cardNumber, card.cardNumber);
                Assert.AreEqual(userId, card.usrId);
                Assert.AreEqual(verficationCode, card.verificationCode);
                Assert.AreEqual(expirationDate, card.expirationDate);
                Assert.AreEqual(type, card.cardType);
                Assert.AreEqual(true, card.defaultCard);

                cardNumber = 22222;
                verficationCode = 333;
                expirationDate = DateTime.Now;
                type = "Debit";
                CardDetails cardDetails2 = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                modelService.AddCard(userId, cardDetails2);

                Card card2 = cardDao.Find(cardNumber);
                // Check data
                Assert.AreEqual(cardNumber, card2.cardNumber);
                Assert.AreEqual(userId, card2.usrId);
                Assert.AreEqual(verficationCode, card2.verificationCode);
                Assert.AreEqual(expirationDate, card2.expirationDate);
                Assert.AreEqual(type, card2.cardType);
                Assert.AreEqual(false, card2.defaultCard);

                userProfileDao.Remove(userId);
                //transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ViewCardByUser
        ///</summary>
        [TestMethod()]
        public void ViewCardByUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and find profile
                long userId =
                    modelService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, address));

                UserProfile userProfile = userProfileDao.Find(userId);

                // Add a cards
                int cardNumber = 11111;
                int verficationCode = 222;
                DateTime expirationDate = DateTime.Now;
                string type = "Credit";
                CardDetails cardDetails = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                modelService.AddCard(userId, cardDetails);

                int cardNumber1 = 22222;
                int verficationCode1 = 333;
                DateTime expirationDate1 = DateTime.Now;
                string type1 = "Debit";
                CardDetails cardDetails2 = new CardDetails(cardNumber1, verficationCode1, expirationDate1, type1);
                modelService.AddCard(userId, cardDetails2);


                // Extract all cards
                List<CardDetails> cards = modelService.ViewCardsByUser(userId);
                // Check data
                Assert.AreEqual(cardNumber, cards[0].CardNumber);
                Assert.AreEqual(verficationCode, cards[0].VerificationCode);
                Assert.AreEqual(expirationDate, cards[0].ExpirateTime);
                Assert.AreEqual(type, cards[0].CardType);

                // Check data
                Assert.AreEqual(cardNumber1, cards[1].CardNumber);
                Assert.AreEqual(verficationCode1, cards[1].VerificationCode);
                Assert.AreEqual(expirationDate1, cards[1].ExpirateTime);
                Assert.AreEqual(type1, cards[1].CardType);

                userProfileDao.Remove(userId);
                //transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ChangeDefaulCard
        ///</summary>
        [TestMethod()]
        public void ChangeDefaultCardTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and find profile
                long userId =
                    modelService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, address));

                UserProfile userProfile = userProfileDao.Find(userId);

                // Add a cards
                int cardNumber = 11111;
                int verficationCode = 222;
                DateTime expirationDate = DateTime.Now;
                string type = "Credit";
                CardDetails cardDetails = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                modelService.AddCard(userId, cardDetails);
                
                int cardNumber1 = 22222;
                int verficationCode1 = 333;
                DateTime expirationDate1 = DateTime.Now;
                string type1 = "Debit";
                CardDetails cardDetails2 = new CardDetails(cardNumber1, verficationCode1, expirationDate1, type1);
                modelService.AddCard(userId, cardDetails2);

                // Change the default card
                modelService.ChangeDefaultCard(userId, cardNumber1);
                Card card2 = cardDao.Find(cardNumber1);
                Card card = cardDao.Find(cardNumber);

                // Check data
                Assert.AreEqual(cardNumber, card.cardNumber);
                Assert.AreEqual(userId, card.usrId);
                Assert.AreEqual(verficationCode, card.verificationCode);
                Assert.AreEqual(expirationDate, card.expirationDate);
                Assert.AreEqual(type, card.cardType);
                Assert.AreEqual(false, card.defaultCard);

                // Check data
                Assert.AreEqual(cardNumber1, card2.cardNumber);
                Assert.AreEqual(userId, card2.usrId);
                Assert.AreEqual(verficationCode1, card2.verificationCode);
                Assert.AreEqual(expirationDate1, card2.expirationDate);
                Assert.AreEqual(type1, card2.cardType);
                Assert.AreEqual(true, card2.defaultCard);

                userProfileDao.Remove(userId);
                //transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for RegisterUser
        ///</summary>
        [TestMethod()]
        public void RegisterUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and find profile
                long userId =
                    modelService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country,address));

                UserProfile userProfile = userProfileDao.Find(userId);
                // Check data
                Assert.AreEqual(userId, userProfile.usrId);
                Assert.AreEqual(loginName, userProfile.loginName);
                Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.enPassword);
                Assert.AreEqual(firstName, userProfile.firstName);
                Assert.AreEqual(lastName, userProfile.lastName);
                Assert.AreEqual(email, userProfile.email);
                Assert.AreEqual(language, userProfile.language);
                Assert.AreEqual(country, userProfile.country);
                Assert.AreEqual(address, userProfile.postalAddress);

                userProfileDao.Remove(userId);
                //transaction.Complete() is not called, so Rollback is executed.
            }
        }
    }
}
