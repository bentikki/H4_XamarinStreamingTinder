using StreamingTinderClassLibrary.Validator.ValidationHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class MinLengthInputValidator : InputValidatorHandler
    {
        private int _stringLength;

        public MinLengthInputValidator(int stringLength)
        {
            _stringLength = stringLength;
        }

        public async override Task Handle(object request)
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
