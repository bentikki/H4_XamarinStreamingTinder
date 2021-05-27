using System;
using System.Collections.Generic;
using System.Text;
using StreamingTinderClassLibrary.Validator.ValidationHandlers;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class PasswordRequirementInputValidator : InputValidatorHandler
    {
        public override void Handle(object request)
        {
            if (request is string)
            {
                string validateString = (string)request;
                byte passwordLength = 6;

                if(validateString.Length < passwordLength)
                    this.ErrorList.Add($"{Name} must be longer than {passwordLength} characters.");

            }

        }
    }
}
