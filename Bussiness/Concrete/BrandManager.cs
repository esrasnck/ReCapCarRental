using Bussiness.Abstract;
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

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

        public Brand GetByBrandId(int id)
        {
            return _brandDal.GetByID(id);
        }

        public void AddBrand(Brand brand)
        {
            _brandDal.Add(brand);
        }

        public void UpdateBrand(Brand brand)
        {
            _brandDal.Update(brand);
        }
        public void DeleteBrand(Brand brand)
        {
            _brandDal.Delete(brand);
        }
    }
}
