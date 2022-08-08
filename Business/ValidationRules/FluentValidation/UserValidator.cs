using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Firstname).NotEmpty();
            RuleFor(u => u.Lastname).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.PasswordHash).NotEmpty();
            RuleFor(u => u.PasswordSalt).NotEmpty();
            RuleFor(u => u.Firstname).MinimumLength(3);
            RuleFor(u => u.Lastname).NotEmpty();
            RuleFor(u => u.Email).Must( e => e.Contains("@")).WithMessage("E-mail bilgisini doğru giriniz.");
        }
    }
}
