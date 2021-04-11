using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
   public interface ICardService
    {
        IResult Add(Card card);
        IResult Update(Card card);
        IResult Delete(Card card);
        IDataResult<List<Card>> GetAllCards();
        IDataResult<Card> GetByCustomerId(int customerId);
        IDataResult<Card> GetbyCardNumber(string cardNumber);

        IDataResult<List<Card>> GetCardListByCustomerId(int customerId);
    }
}
