using System;
using System.Collections.Generic;
using System.Text;
using StreamingTinderClassLibrary.Validator.ValidationHandlers;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class NullInputValidator : InputValidatorHandler
    {
        public override void Handle(object request)
        {
            string errorList = $"{this.Name} must not be null";
            if (request == null)
            {
                this.stopValidation = true;
                this.ErrorList.Add(errorList);
            }

            if (request is string validateString)
            {
                if (string.IsNullOrWhiteSpace(validateString) || string.IsNullOrEmpty(validateString))
                {
                    this.stopValidation = true;
                    this.ErrorList.Add(errorList);
                }   
            }

        }
    }
}
