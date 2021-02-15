using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll();
        IDataResult<Brand> GetByBrandId(int id);
        IResult AddBrand(Brand brand);
        IResult UpdateBrand(Brand car);
        IResult DeleteBrand(Brand car);

        
       
    }
}
