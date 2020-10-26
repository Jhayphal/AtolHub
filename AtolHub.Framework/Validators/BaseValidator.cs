using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtolHub.Framework.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {

        protected BaseValidator(IEnumerable<IValidatorConsumer<T>> validators)
        {
            PostInitialize(validators);
        }

        protected virtual void PostInitialize(IEnumerable<IValidatorConsumer<T>> validators)
        {
            foreach (var item in validators)
            {
                item.AddRules(this);
            }
        }
    }
}
