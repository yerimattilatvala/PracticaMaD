using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.Util;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Transactional]
        public long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails)
        {

            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);

            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.language = userProfileDetails.Language;
                userProfile.country = userProfileDetails.Country;
                userProfile.postalAddress = userProfileDetails.PostalAddress;
                try
                {
                    UserProfileDao.Create(userProfile);
                } catch (SqlException)
                {
                    throw new SqlException("");
                }

                return userProfile.usrId;

            }

        }

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>        
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.usrId, userProfile.firstName,
                storedPassword, userProfile.language, userProfile.country);
        }

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language, userProfile.country, userProfile.postalAddress);

            return userProfileDetails;
        }

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile =
                UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.language = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            UserProfileDao.Update(userProfile);
        }

        [Transactional]
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
                PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }
    }
}
