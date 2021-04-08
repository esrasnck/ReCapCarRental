using Bussiness.Abstract;
using Bussiness.Business.BusinessAspects.Autofac;
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

            var result = BusinessRules.Run(CheckIfColorExist(id));
            if (result != null)
            {
                return new ErrorDataResult<Color>(Messages.NotColorByID);
            }
            return new SuccessDataResult<Color>(_colorDal.Get(x => x.ColorId == id), Messages.ColorByID);

        }


        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult AddAColor(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorNameExists(color.ColorName));
            if (result != null)
            {
                return new ErrorResult(Messages.ColorAlreadyExist);
            }
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }


        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult DeleteColor(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorExist(color.ColorId));
            if (result != null)
            {
                return new ErrorResult(Messages.CarNotDeleted);
            }
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);

        }


        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult UpdateColor(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorExist(color.ColorId));
            if (result != null)
            {
                return new ErrorResult(Messages.ColorNotUpdated);
            }
            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }


        private IResult CheckIfColorNameExists(string colorName)
        {
            if (!String.IsNullOrEmpty(colorName))
            {
                var result = _colorDal.Any(x => x.ColorName.ToLower().Contains(colorName.ToLower()));
                if (!result)
                {
                    return new SuccessResult();
                }
            }

            return new ErrorResult();
        }

        private IResult CheckIfColorExist(int colorId)
        {
            var result = _colorDal.Any(x => x.ColorId == colorId);
            if (!result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

    }
}
