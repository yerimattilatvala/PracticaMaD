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
        public Order FindByLoginName(string loginName)
        {
            Order userProfile = null;

            #region Option 1: Using Linq.

            DbSet<Order> userProfiles = Context.Set<Order>();

            //var result =
            //    (from u in userProfiles
            //     where u.loginName == loginName
            //     select u);

            //userProfile = result.FirstOrDefault();

            #endregion

            #region Option 2: Using eSQL over dbSet

            string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
            DbParameter loginNameParameter =
                new System.Data.SqlClient.SqlParameter("loginName", loginName);

            userProfile = Context.Database.SqlQuery<Order>(sqlQuery, loginNameParameter).FirstOrDefault<Order>();

            #endregion

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            //String sqlQuery =
            //    "SELECT VALUE u FROM MiniPortalEntities.UserProfiles AS u " +
            //    "WHERE u.loginName=@loginName";

            //ObjectParameter param = new ObjectParameter("loginName", loginName);

            //ObjectQuery<UserProfile> query =
            //  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserProfile>(sqlQuery, param);

            //var result = query.Execute(MergeOption.AppendOnly);

            //try
            //{
            //    userProfile = result.First<UserProfile>();
            //}
            //catch (Exception)
            //{
            //    userProfile = null;
            //}

            #endregion

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(Order).FullName);

            return userProfile;
        }
    }
}
