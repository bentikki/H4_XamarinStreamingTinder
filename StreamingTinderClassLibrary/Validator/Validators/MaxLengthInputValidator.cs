using StreamingTinderClassLibrary.Validator.ValidationHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class MaxLengthInputValidator : InputValidatorHandler
    {
        private int _stringLength;

        public MaxLengthInputValidator(int stringLength)
        {
            _stringLength = stringLength;
        }

        public override void Handle(object request)
        {
            if(request is string validateString)
            {
                if(validateString.Length > _stringLength)
                {
                    ErrorList.Add($"{Name} must be longer than {_stringLength} characters");
                }
            }
        }
        
    }
}
