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
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorListed);
        }

        public IDataResult<Color> GetByColorID(int id)
        {
            if (id > 0 && _colorDal.Any(x => x.ColorId == id))
            {
                return new SuccessDataResult<Color>(_colorDal.Get(x => x.ColorId == id), Messages.ColorByID);
            }
            return new ErrorDataResult<Color>(Messages.NotColorByID);

        }

       // [CacheRemoveAspect("IColorService.Get")]
        public IResult AddAColor(Color color)
        {
            if (color != null)
            {
                if (!_colorDal.Any(x => x.ColorName.Contains(color.ColorName)))
                {
                    _colorDal.Add(color);
                    return new SuccessResult(Messages.ColorAdded);
                }
                return new ErrorResult(Messages.ColorAlreadyExist);
            }
            return new ErrorResult(Messages.ColorNotAdded);

        }
      //  [CacheRemoveAspect("IColorService.Get")]
        public IResult DeleteColor(Color color)
        {
            if (color != null)
            {
                if (_colorDal.Any(x=> x.ColorId == color.ColorId))
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
                return new ErrorResult(Messages.ColorNotDeleted);
            }
            return new ErrorResult(Messages.ColorNotDeleted);
        }

       // [CacheRemoveAspect("IColorService.Get")]
        public IResult UpdateColor(Color color)
        {
            if (color != null)
            {
                if (color.ColorId>0 && _colorDal.Any(x=> x.ColorId == color.ColorId))
                {
                    Color colorFind = _colorDal.GetByID(color.ColorId);
                    if (colorFind == null)
                    {
                        return new ErrorResult(Messages.CarNotUpdated);
                    }
                    else
                    {
                        if (color.ColorName != null)
                        {
                            colorFind.ColorName = color.ColorName;
                            _colorDal.Update(colorFind);
                            return new SuccessResult(Messages.ColorUpdated);
                        }
                        return new ErrorResult(Messages.ColorNotUpdated);
                    }
                }
                return new ErrorResult(Messages.ColorNotUpdated);
           
        
            }
            return new ErrorResult(Messages.ColorNotUpdated);
        }




    }
}
