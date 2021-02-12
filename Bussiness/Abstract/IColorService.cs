using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
   public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetByColorID(int id);
        IResult AddAColor(Color color);
        IResult UpdateColor(Color color);
        IResult DeleteColor(Color color);
    }

}
