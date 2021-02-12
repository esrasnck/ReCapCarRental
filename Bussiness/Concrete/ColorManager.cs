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
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>> (_colorDal.GetAll(),Messages.ColorListed);
        }

        public IDataResult<Color> GetByColorID(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(x=> x.ColorId==id),Messages.ColorByID);
        }
        public IResult AddAColor(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        public IResult DeleteColor(Color color)
        {
            Color colorFind = _colorDal.GetByID(color.ColorId);
            if (colorFind == null)
            {
                return new ErrorResult(Messages.CarNotDeleted);
            }
            else
            {
                _colorDal.Delete(color);
                return new SuccessResult(Messages.ColorDeleted);
            }
        }

        public IResult UpdateColor(Color color)
        {
            Color colorFind = _colorDal.GetByID(color.ColorId);
            if (colorFind == null)
            {
                return new ErrorResult(Messages.CarNotUpdated);
            }
            else
            {
                _colorDal.Update(color);
                return new SuccessResult(Messages.ColorUpdated);
            }
        }




    }
}
