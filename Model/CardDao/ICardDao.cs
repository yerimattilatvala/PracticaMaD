using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CardDao
{
    public interface ICardDao : IGenericDao<Card, Int64>
    {
        Card FindByCardNumber(string CardNumber);
    }
}
