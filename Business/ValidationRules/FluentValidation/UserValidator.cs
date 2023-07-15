using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        //public UserValidator()
        //{
        //    RuleFor(u => u.FirstName).MinimumLength(2);
        //    RuleFor(u => u.LastName).MinimumLength(2);
        //    RuleFor(u => u.Email).EmailAddress();
        //    RuleFor(u => u.Password).MinimumLength(8);
        //    RuleFor(u => u.Password).Must(ContainLetter).WithMessage("Şifre en az bir harf içermeli");
        //    RuleFor(u => u.Password).Must(ContainDigit).WithMessage("Şifre en az bir sayı içermeli");
        //}

        //private bool ContainDigit(string arg)
        //{
        //    return arg.Any(char.IsDigit);
        //}

        //private bool ContainLetter(string arg)
        //{
        //    return arg.Any(char.IsLetter);
        //}
    }

}
