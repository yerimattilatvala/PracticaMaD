using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
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
        public List<CardDetails> ViewCardsByUser(long userProfileId)
        {
            List<CardDetails> userCards = new List<CardDetails>();

            List<Card> cards = UserProfileDao.Find(userProfileId).Cards.ToList<Card>();

            for (int i = 0; i < cards.Count; i++)
            {
                string cardNumber = cards.ElementAt(i).cardNumber;
                int cv = cards.ElementAt(i).verificationCode;
                DateTime expirationDate = cards.ElementAt(i).expirationDate;
                string type = cards.ElementAt(i).cardType;
                bool defaultCard = cards.ElementAt(i).defaultCard;
                long cardId = cards.ElementAt(i).idCard;
                userCards.Add(new CardDetails(cardNumber, cv, expirationDate, type,cardId,defaultCard));
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
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            } catch (InstanceNotFoundException e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {
                new InstanceNotFoundException(userProfileId,"Usuario no encontrado");
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
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            } catch(InstanceNotFoundException e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {
                new InstanceNotFoundException(cardID, "Trajeta no encontrada");
            }
            card.defaultCard = true;
            CardDao.Update(card);
        }
    }

}
