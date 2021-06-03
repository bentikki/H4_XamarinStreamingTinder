using StreamingTinderClassLibrary.Validator.ValidationHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class ExactLengthInputValidator : InputValidatorHandler
    {
        private int _stringLength;

        public ExactLengthInputValidator(int stringLength)
        {
            _stringLength = stringLength;
        }

        public async override Task Handle(object request)
        {
            if(request is string validateString)
            {
                if(validateString.Length != _stringLength)
                {
                    ErrorList.Add($"{Name} must be exactly {_stringLength} characters");
                }
            }
        }
        
    }
}
