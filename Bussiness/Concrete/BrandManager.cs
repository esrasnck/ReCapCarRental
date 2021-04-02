using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandListed);
        }

        public IDataResult<Brand> GetByBrandId(int id)
        {
            var result = BusinessRules.Run(CheckIfBrandExist(id));
            if (result != null)
            {
                return new ErrorDataResult<Brand>(Messages.BrandNotListed);
            }
            return new SuccessDataResult<Brand>(_brandDal.Get(x => x.BrandId == id), Messages.BrandByID);

        }


        // secured option kaldı.
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult AddBrand(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));
            if (result!=null)
            {
                return new ErrorResult(Messages.BrandAllreadyExist);
            }
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }


        // secured option kaldı.

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult UpdateBrand(Brand brand)// azcık kal sen burada
        {
            var result = BusinessRules.Run(CheckIfBrandExist(brand.BrandId));
            if (result != null)
            {
                return new ErrorResult(Messages.BrandNotUpdated);
            }
            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);

        }


        // secured option kaldı.

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult DeleteBrand(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandExist(brand.BrandId));
            if (result != null)
            {
                return new ErrorResult(Messages.BrandNotDeleted);
            }
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);

        }


        private IResult CheckIfBrandNameExists(string brandName)
        {
            if (!string.IsNullOrEmpty(brandName))
            {
                var result = _brandDal.Any(x => x.BrandName.ToLower().Contains(brandName.ToLower()));

                if (!result)
                {
                    return new SuccessResult();
                }
            }
           
           return new ErrorResult();
        }

        private IResult CheckIfBrandExist(int brandId)
        {
            var result = _brandDal.Any(x => x.BrandId == brandId);
            if (!result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }


    }
}
