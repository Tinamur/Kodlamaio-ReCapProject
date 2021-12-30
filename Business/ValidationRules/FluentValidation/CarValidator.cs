using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    //fluentValidation dan bir AbstractValidator al
    //Car için ayarla
    //dto için de ayarlayabilirsin
    public class CarValidator : AbstractValidator<Car>
    {
        //Validation kuralları ctor içine yazılır
        public CarValidator()
        {
            RuleFor(c => c.CarName).NotEmpty();
            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.ColorId).NotEmpty();
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.CarName).MinimumLength(2);
            RuleFor(c => c.DailyPrice).GreaterThan(0);
            
        }
    }
}
