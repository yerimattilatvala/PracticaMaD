using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {

        UserProfile FindByLoginName(String loginName);


    }
}
