using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(5);
            //    RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(250);
            //    RuleFor(c => c.Description).Must(StartWithA).WithMessage("Araç isimleri A ile başlamalıdır.");
            //}

            //private bool StartWithA(string arg)
            //{
            //    return arg.StartsWith("A");
        }
    }
}
