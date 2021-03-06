﻿using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CardDao
{
    public class CardDaoEntityFramework : GenericDaoEntityFramework<Card, Int64>, ICardDao
    {
        public Card FindByCardNumber(string CardNumber)
        {
            Card card = null;

            string sqlQuery = "Select * FROM Card where cardNumber=@cardNumber";
            DbParameter cardNumberParameter =
                new System.Data.SqlClient.SqlParameter("cardNumber", CardNumber);

            card = Context.Database.SqlQuery<Card>(sqlQuery, cardNumberParameter).FirstOrDefault<Card>();

            if (card == null)
                throw new InstanceNotFoundException(CardNumber,
                    typeof(Card).FullName);

            return card;
        }
    }
}
