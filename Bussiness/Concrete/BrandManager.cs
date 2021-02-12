using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class BrandManager:IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.BrandListed);
        }

        public IDataResult<Brand> GetByBrandId(int id)
        {
            return new SuccessDataResult<Brand> (_brandDal.Get(x=> x.BrandId == id),Messages.BrandByID);
        }

        public IResult AddBrand(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        public IResult UpdateBrand(Brand brand)// azcık kal sen burada
        {
            Brand brandFind = _brandDal.GetByID(brand.BrandId);
            if (brandFind==null)
            {
                return new ErrorResult(Messages.BrandNotDeleted);

            }
            else
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandAdded);
            }
            
           
        }
        public IResult DeleteBrand(Brand brand)  // dönecem ben sana
        {
            Brand brandFind = _brandDal.GetByID(brand.BrandId);
            if (brandFind == null)
            {
                return new ErrorResult(Messages.BrandNotDeleted);
            }
            else
            {
                _brandDal.Delete(brand);
                return new SuccessResult(Messages.BrandDeleted);
            }
           
        }
    }
}
