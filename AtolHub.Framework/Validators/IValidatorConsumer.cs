using System;
using System.Collections.Generic;
using System.Text;

namespace AtolHub.Framework.Validators
{
    public interface IValidatorConsumer<T> where T : class
    {
        void AddRules(BaseValidator<T> validator);
    }
}
