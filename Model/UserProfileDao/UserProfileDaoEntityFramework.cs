using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {
        public UserProfile FindByEmail(string email)
        {
            UserProfile userProfile = null;

            #region Option 2: Using eSQL over dbSet

            string sqlQuery = "Select * FROM UserProfile where email=@email";
            DbParameter emailNameParameter =
                new System.Data.SqlClient.SqlParameter("email", email);

            userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, emailNameParameter).FirstOrDefault<UserProfile>();

            #endregion

            if (userProfile == null)
                throw new InstanceNotFoundException(email,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            #region Option 2: Using eSQL over dbSet

            string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
            DbParameter loginNameParameter =
                new System.Data.SqlClient.SqlParameter("loginName", loginName);

            userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, loginNameParameter).FirstOrDefault<UserProfile>();

            #endregion

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }
    }
}
