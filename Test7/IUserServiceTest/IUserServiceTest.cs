using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Util;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.UserService;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Test.IUserServiceTest
{
    /// <summary>
    /// Descripción resumida de IUserServiceTest
    /// </summary>
    [TestClass]
    public class IUserServiceTest
    {
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const int postalAddress = 36645;
        private const long NON_EXISTENT_USER_ID = -1;

        TransactionScope transaction;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

            userProfileDao = kernel.Get<IUserProfileDao>();
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
        ///A test for RegisterUser
        ///</summary>
        [TestMethod()]
        public void RegisterUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and find profile
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

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

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for registering a user that already exists in the database
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                // Register the same user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod()]
        public void LoginClearPasswordTest()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                LoginResult expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with clear password
                LoginResult actual =
                       userService.Login(loginName,
                       clearPassword, false);

                // Check data
                Assert.AreEqual(expected, actual);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod()]
        public void LoginEncryptedPasswordTest()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                LoginResult expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with encrypted password
                LoginResult obtained =
                       userService.Login(loginName,
                       PasswordEncrypter.Crypt(clearPassword), true);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.

            }
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod()]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                // Login with incorrect (clear) password
                LoginResult actual =
                       userService.Login(loginName, clearPassword + "X", false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            // Login for a user that has not been registered
            LoginResult actual =
                   userService.Login(loginName, clearPassword, false);
        }

        /// <summary>
        ///A test for FindUserProfileDetails
        ///</summary>
        [TestMethod()]
        public void FindUserProfileDetailsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserProfileDetails expected =
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress);

                long userId =
                    userService.RegisterUser(loginName, clearPassword, expected);

                UserProfileDetails obtained =
                       userService.FindUserProfileDetails(userId);

                // Check data
                Assert.AreEqual(expected.FirstName, obtained.FirstName);
                Assert.AreEqual(expected.Lastname, obtained.Lastname);
                Assert.AreEqual(expected.Email, obtained.Email);
                Assert.AreEqual(expected.Country, obtained.Country);
                Assert.AreEqual(expected.Language, obtained.Language);
                Assert.AreEqual(expected.PostalAddress, obtained.PostalAddress);
                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for FindUserProfileDetails when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserProfileDetailsForNonExistingUserTest()
        {
            userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails
        ///</summary>
        [TestMethod()]
        public void UpdateUserProfileDetailsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and update profile details
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                UserProfileDetails expected =
                        new UserProfileDetails(firstName + "X", lastName + "X",
                            email + "X", "XX", "XX", postalAddress);

                userService.UpdateUserProfileDetails(userId, expected);

                UserProfileDetails obtained =
                    userService.FindUserProfileDetails(userId);

                // Check changes
                //Assert.AreEqual(expected, obtained);
                Assert.AreEqual(expected.FirstName, obtained.FirstName);
                Assert.AreEqual(expected.Lastname, obtained.Lastname);
                Assert.AreEqual(expected.Email, obtained.Email);
                Assert.AreEqual(expected.Country, obtained.Country);
                Assert.AreEqual(expected.Language, obtained.Language);
                Assert.AreEqual(expected.PostalAddress, obtained.PostalAddress);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserProfileDetailsForNonExistingUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod()]
        public void ChangePasswordTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                // Change password
                String newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword, newClearPassword);

                // Try to login with the new password. If the login is correct, then
                // the password was successfully changed.
                userService.Login(loginName, newClearPassword, false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ChangePassword entering a wrong old password
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country, postalAddress));

                // Change password
                String newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ChangePassword when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }
    }
}
