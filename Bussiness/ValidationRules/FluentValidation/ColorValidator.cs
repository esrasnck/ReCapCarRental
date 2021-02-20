using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Bussiness.ValidationRules.FluentValidation
{
    public class ColorValidator: AbstractValidator<Color>
    {
        public ColorValidator()
        {
                
        }
    }
}
