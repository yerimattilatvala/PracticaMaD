using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.UserService;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Test.ICardServiceTest
{
    /// <summary>
    /// Descripción resumida de ICardServiceTest
    /// </summary>
    [TestClass]
    public class ICardServiceTest
    {
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

        private static IKernel kernel;
        private static ICardDao cardDao;
        private static IUserProfileDao userProfileDao;
        private static ICardService cardService;
        private static IUserService userService;

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
            cardDao = kernel.Get<ICardDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            cardService = kernel.Get<ICardService>();
            userService = kernel.Get<IUserService>();
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
        ///A test for AddCard
        ///</summary>
        [TestMethod()]
        public void AddCardTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and find profile
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, address));

                UserProfile userProfile = userProfileDao.Find(userId);

                // Add a card and find
                string cardNumber = "11111";
                int verficationCode = 222;
                DateTime expirationDate = DateTime.Now;
                string type = "Credit";
                CardDetails cardDetails = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                cardService.AddCard(userId, cardDetails);

                Card card = cardDao.FindByCardNumber(cardNumber);
                // Check data
                Assert.AreEqual(cardNumber, card.cardNumber);
                Assert.AreEqual(userId, card.usrId);
                Assert.AreEqual(verficationCode, card.verificationCode);
                Assert.AreEqual(expirationDate, card.expirationDate);
                Assert.AreEqual(type, card.cardType);
                Assert.AreEqual(true, card.defaultCard);

                cardNumber = "22222";
                verficationCode = 333;
                expirationDate = DateTime.Now;
                type = "Debit";
                CardDetails cardDetails2 = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                cardService.AddCard(userId, cardDetails2);

                Card card2 = cardDao.FindByCardNumber(cardNumber);
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
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, address));

                UserProfile userProfile = userProfileDao.Find(userId);

                // Add a cards
                string cardNumber = "11111";
                int verficationCode = 222;
                DateTime expirationDate = DateTime.Now;
                string type = "Credit";
                CardDetails cardDetails = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                cardService.AddCard(userId, cardDetails);

                string cardNumber1 = "22222";
                int verficationCode1 = 333;
                DateTime expirationDate1 = DateTime.Now;
                string type1 = "Debit";
                CardDetails cardDetails2 = new CardDetails(cardNumber1, verficationCode1, expirationDate1, type1);
                cardService.AddCard(userId, cardDetails2);


                // Extract all cards
                List<CardDetails> cards = cardService.ViewCardsByUser(userId);
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
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, address));

                UserProfile userProfile = userProfileDao.Find(userId);

                // Add a cards
                string cardNumber = "11111";
                int verficationCode = 222;
                DateTime expirationDate = DateTime.Now;
                string type = "Credit";
                CardDetails cardDetails = new CardDetails(cardNumber, verficationCode, expirationDate, type);
                cardService.AddCard(userId, cardDetails);

                string cardNumber1 = "22222";
                int verficationCode1 = 333;
                DateTime expirationDate1 = DateTime.Now;
                string type1 = "Debit";
                CardDetails cardDetails2 = new CardDetails(cardNumber1, verficationCode1, expirationDate1, type1);
                cardService.AddCard(userId, cardDetails2);

                // Change the default card
                Card card2 = cardDao.FindByCardNumber(cardNumber1);
                cardService.ChangeDefaultCard(userId, card2.idCard);
                
                Card card = cardDao.FindByCardNumber(cardNumber);
                card2 = cardDao.FindByCardNumber(cardNumber1);

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
    }
}
