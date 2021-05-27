using StreamingTinderClassLibrary.Validator.ValidationHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class MinLengthInputValidator : InputValidatorHandler
    {
        private int _stringLength;

        public MinLengthInputValidator(int stringLength)
        {
            _stringLength = stringLength;
        }

        public override void Handle(object request)
        {
            if(request is string validateString)
            {
                if(validateString.Length < _stringLength)
                {
                    ErrorList.Add($"{Name} must be shorter than {_stringLength} characters");
                }
            }
        }
        
    }
}
