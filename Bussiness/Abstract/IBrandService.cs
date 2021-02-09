using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand GetByBrandId(int id);
        void AddBrand(Brand brand);
        void UpdateBrand(Brand car);
        void DeleteBrand(Brand car);
       
    }
}
