using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.UserService
{
    public interface IUserService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserProfileDetails userProfileDetails);

        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        [Transactional]
        void ChangePassword(long userProfileId, String oldClearPassword,
            String newClearPassword);
    }
}
