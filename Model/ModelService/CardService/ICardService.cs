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
    public interface ICardService
    {

        [Inject]
        ICardDao CardDao { set; }

        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Transactional]
        void AddCard(long userProfileId, CardDetails newCard);

        [Transactional]
        List<CardDetails> ViewCardsByUser(long userProfileId);

        [Transactional]
        void ChangeDefaultCard(long userProfileId, long cardId);
    }
}
