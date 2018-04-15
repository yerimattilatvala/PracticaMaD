using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CardDao
{
    public class CardDaoEntityFramework : GenericDaoEntityFramework<Card, int>, ICardDao
    {
    }
}
