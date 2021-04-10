using Bussiness.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Concrete
{
   public class PaymentManager:IPaymentService
    {
        IPaymentDal _paymentDal;
        ICardService _cardService;
        public PaymentManager(IPaymentDal paymentDal,ICardService cardService)
        {
            _paymentDal = paymentDal;
            _cardService = cardService;
           
        }

        public IResult Add(Payment payment)
        {
            var result = BusinessRules.Run(CheckIsCreditCardExist(payment.CreditCardNumber, payment.ExpirationDate, payment.SecurityCode));

            if (result != null)
            {
                return result;
            }
            _paymentDal.Add(payment);

            return new SuccessResult();
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();
        }

        public IDataResult<Payment> Get(Payment payment)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(x => x.PaymentId== payment.PaymentId));
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<Payment> GetByPaymentId(int paymentId)
        {
            var result = _paymentDal.Get(x => x.PaymentId == paymentId);
            if (result==null)
            {
                return new ErrorDataResult<Payment>();
            }
            return new SuccessDataResult<Payment>(result);
        }

        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult();
        }

        private IResult CheckIsCreditCardExist(string cardNumber, string expirationDate, string securityCode)
        {
            if (!_cardService.GetAllCards().Data.Any(x => x.CreditCardNumber == cardNumber &&  x.ExpirationDate == expirationDate && x.SecurityCode == securityCode))
            {
                return new ErrorResult("Kredi Kartı Bilgileri Yanlış");
            }
            return new SuccessResult();
        }
    }
}
