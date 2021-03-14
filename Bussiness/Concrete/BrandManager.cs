using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Aspects.Autofac.Caching;
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
            if (id > 0 && _brandDal.Any(x => x.BrandId == id))
            {
                return new SuccessDataResult<Brand>(_brandDal.Get(x => x.BrandId == id), Messages.BrandByID);
            }
            return new ErrorDataResult<Brand>(Messages.NotBrandByID);

        }

     //   [CacheRemoveAspect("IBrandService.Get")]
        public IResult AddBrand(Brand brand)
        {
            if (brand != null)
            {
                if (!_brandDal.Any(x => x.BrandName.Contains(brand.BrandName)))
                {
                    _brandDal.Add(brand);
                    return new SuccessResult(Messages.BrandAdded);
                }
                return new ErrorResult(Messages.BrandAllreadyExist);
            }
            return new ErrorResult(Messages.BrandNotAdded);

        }

      //  [CacheRemoveAspect("IBrandService.Get")]
        public IResult UpdateBrand(Brand brand)// azcık kal sen burada
        {
            if (brand != null)
            {
                if (_brandDal.Any(x => x.BrandId == brand.BrandId) || _brandDal.Any(x => x.BrandName.Contains(brand.BrandName)))
                {
                    if (brand.BrandId > 0)
                    {
                        Brand brandFind = _brandDal.GetByID(brand.BrandId);
                        if (brandFind == null)
                        {
                            return new ErrorResult(Messages.BrandNotUpdated);

                        }
                        else
                        {
                            if (brand.BrandName != null)
                            {
                                brandFind.BrandName = brand.BrandName;
                                _brandDal.Update(brandFind);
                                return new SuccessResult(Messages.BrandUpdated);
                            }
                            return new ErrorResult(Messages.BrandNotUpdated);
                        }
                    }
                    #region forFun:)
                    //else
                    //{
                    //    Brand brandFind = _brandDal.FirstOrDefault(x=> x.BrandName == brand.BrandName);
                    //    if (brandFind == null)
                    //    {
                    //        return new ErrorResult(Messages.BrandNotUpdated);

                    //    }
                    //    else
                    //    {
                    //        if (brand.BrandName != null)
                    //        {
                    //            brandFind.BrandName = brand.BrandName;
                    //            _brandDal.Update(brandFind);
                    //            return new SuccessResult(Messages.BrandUpdated);
                    //        }
                    //        return new ErrorResult(Messages.BrandNotUpdated);
                    //    }
                    //}
                    #endregion
                }
            }
            return new ErrorResult(Messages.BrandNotUpdated);
        }

       // [CacheRemoveAspect("IBrandService.Get")]
        public IResult DeleteBrand(Brand brand) 
        {
            if (brand != null)
            {
                if (_brandDal.Any(x => x.BrandId == brand.BrandId))
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
                return new ErrorResult(Messages.BrandNotDeleted);
            }
            return new ErrorResult(Messages.BrandNotDeleted);
        }
    }
}
