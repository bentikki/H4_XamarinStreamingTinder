using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StreamingTinderClassLibrary.Validator.ValidationHandlers;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class NoSpaceInputValidator : InputValidatorHandler
    {
        public async override Task Handle(object request)
        {
            if(request is string)
            {
                string validateString = (string)request;

                if (Regex.IsMatch(validateString, @"\s"))
                {
                    this.ErrorList.Add($"{this.Name} must not contain any spaces");
                }
            }   
        }
    }
}
