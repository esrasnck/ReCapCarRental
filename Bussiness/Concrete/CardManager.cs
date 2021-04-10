using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class CardManager : ICardService
    {

        ICardDal _cardDal;
        public CardManager(ICardDal cardDal)
        {
            _cardDal = cardDal;
        }
        public IResult Add(Card card)
        {
            var result = BusinessRules.Run(CheckIsCreditCardExist(card.CreditCardNumber, card.ExpirationDate, card.SecurityCode));
            if (result == null)
            {
                return new ErrorResult();
            }
            _cardDal.Add(card);
            return new SuccessResult(Messages.CardAdded);
        }

        public IResult Delete(Card card)
        {
            _cardDal.Delete(card);
            return new SuccessResult(Messages.CardDeleted);
        }

        public IDataResult<List<Card>> GetAllCards()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll(), Messages.CardListed);
        }

        public IDataResult<Card> GetByCustomerId(int id)
        {
            return new SuccessDataResult<Card>(_cardDal.Get(c => c.CustomerId == id));
        }

        public IResult Update(Card card)
        {
            _cardDal.Update(card);
            return new SuccessResult(Messages.CardUpdated);
        }
        public IDataResult<Card> GetbyCardNumber(string cardNumber)
        {
            var getCardNumber = _cardDal.Get(u => u.CreditCardNumber == cardNumber);
            return new SuccessDataResult<Card>(getCardNumber);
        }

        private IResult CheckIsCreditCardExist(string cardNumber, string expirationDate, string securityCode)
        {
            if (!_cardDal.Any(x => x.CreditCardNumber == cardNumber && x.ExpirationDate == expirationDate && x.SecurityCode == securityCode))
            {
                return new ErrorResult("Kredi Kartı Mevcut");
            }
            return new SuccessResult();
        }


    }
}
