using Bussiness.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public List<Color> GetAll()
        {
            return _colorDal.GetAll();
        }

        public Color GetByColorID(int id)
        {
            return _colorDal.GetByID(id);
        }
        public void AddAColor(Color color)
        {
            _colorDal.Add(color);
        }

        public void DeleteColor(Color color)
        {
            Color colorFind = _colorDal.GetByID(color.ColorId);
            if (colorFind == null)
            {
                Console.WriteLine("silinecek renk yoktur");
            }
            else
            {
                _colorDal.Delete(color);
            }
        }

        public void UpdateColor(Color color)
        {
            Color colorFind = _colorDal.GetByID(color.ColorId);
            if (colorFind == null)
            {
                Console.WriteLine("güncellenecek renk yoktur");
            }
            else
            {
                _colorDal.Update(color);
            }
        }




    }
}
