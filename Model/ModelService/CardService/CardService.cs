﻿using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService
{
    public class CardService : ICardService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]
        public ICardDao CardDao { private get; set; }

        [Transactional]
        public void AddCard(long userProfileId, CardDetails newCard)
        {
            UserProfile UserProfile = UserProfileDao.Find(userProfileId);

            try
            {
                CardDao.FindByCardNumber(newCard.CardNumber);
                throw new DuplicateInstanceException(newCard.CardNumber,
                    typeof(Card).FullName);

            } catch (InstanceNotFoundException)
            {
                long number1 = 0;
                bool canConvert = long.TryParse(newCard.CardNumber, out number1);
                if (!canConvert)
                    throw new IncorrectCardNumberFormatException(newCard.CardNumber);
                Card creditCard = new Card();
                creditCard.usrId = UserProfile.usrId;
                creditCard.cardNumber = newCard.CardNumber;
                creditCard.verificationCode = newCard.VerificationCode;
                creditCard.expirationDate = newCard.ExpirateTime;
                creditCard.cardType = newCard.CardType;

                if (!UserProfile.Cards.Any())
                    creditCard.defaultCard = true;
                else
                    creditCard.defaultCard = false;

                CardDao.Create(creditCard);
            }
        }

        [Transactional]
        public List<CardDetails> ViewCardsByUser(long userProfileId, int startIndex, int count)
        {
            List<CardDetails> userCards = new List<CardDetails>();

            List<Card> cards = null;
            UserProfile user = null;
            try
            {
                user = UserProfileDao.Find(userProfileId);
            }
            catch (InstanceNotFoundException)
            {
                throw new InstanceNotFoundException(userProfileId,"User not found");
            }

            cards = user.Cards.ToList<Card>();
            int c = 0;
            for (int i = startIndex; i < cards.Count; i++)
            {
                if (c == count)
                    break;
                string cardNumber = cards.ElementAt(i).cardNumber;
                int cv = cards.ElementAt(i).verificationCode;
                DateTime expirationDate = cards.ElementAt(i).expirationDate;
                string type = cards.ElementAt(i).cardType;
                bool defaultCard = cards.ElementAt(i).defaultCard;
                long cardId = cards.ElementAt(i).idCard;
                userCards.Add(new CardDetails(cardNumber, cv, expirationDate, type,cardId,defaultCard));
                c++;
            }
            return userCards;
        }

        [Transactional]
        public void ChangeDefaultCard(long userProfileId, long cardID)
        {
            UserProfile userProfile = null;
            Card card = null;
            try
            {
                userProfile = UserProfileDao.Find(userProfileId);
            } catch (InstanceNotFoundException e)
            {
                throw new InstanceNotFoundException(userProfileId,"User not found");
            }

            List<Card> userCards = userProfile.Cards.ToList<Card>();

            for (int i = 0; i < userCards.Count; i++)
            {
                if (userCards.ElementAt(i).defaultCard == true)
                {
                    userCards.ElementAt(i).defaultCard = false;
                    CardDao.Update(userCards.ElementAt(i));
                }
            }
            try
            {

                card = CardDao.Find(cardID);
            } catch(InstanceNotFoundException e)
            {
                throw new InstanceNotFoundException(cardID, "Trajeta no encontrada");
            }
            card.defaultCard = true;
            CardDao.Update(card);
        }

        public int GetNumberOfCardsByUser(long userProfileId)
        {
            int n = 0;
            try
            {
                n = UserProfileDao.Find(userProfileId).Cards.ToList<Card>().Count;
            }
            catch (InstanceNotFoundException e){
                throw new InstanceNotFoundException(userProfileId, "Usuario no encontrado");
            }
            return n;
        }

        public CardDetails GetUserDefaultCard(long userProfileId)
        {
            CardDetails defaultCard = null;
            UserProfile user = null;
            try
            {
                user = UserProfileDao.Find(userProfileId);
            } catch (InstanceNotFoundException e)
            {
                throw new InstanceNotFoundException(userProfileId,"Usuario no encontrado");
            }
            List<Card> userCards = user.Cards.ToList();
            if (userCards != null) {
                for (int i = 0; i < userCards.Count; i++)
                {
                    if (userCards.ElementAt(i).defaultCard)
                    {
                        string cardNumber = userCards.ElementAt(i).cardNumber;
                        string cardType = userCards.ElementAt(i).cardType;
                        int cv = userCards.ElementAt(i).verificationCode;
                        bool defaultC = userCards.ElementAt(i).defaultCard;
                        long cardId = userCards.ElementAt(i).idCard;
                        DateTime date = userCards.ElementAt(i).expirationDate;
                        defaultCard = new CardDetails(cardNumber, cv, date, cardType, cardId, defaultC);
                    }
                }
            }
            return defaultCard;
        }
    }

}
