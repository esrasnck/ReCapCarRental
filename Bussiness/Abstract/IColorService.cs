using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
   public interface IColorService
    {
        List<Color> GetAll();
        Color GetByColorID(int id);
        void AddAColor(Color color);
        void UpdateColor(Color color);
        void DeleteColor(Color color);
      

    }
}
