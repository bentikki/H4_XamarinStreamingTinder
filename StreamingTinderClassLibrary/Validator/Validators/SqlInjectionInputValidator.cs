using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using StreamingTinderClassLibrary.Validator.ValidationHandlers;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class SqlInjectionInputValidator : InputValidatorHandler
    {
        public override void Handle(object request)
        {
            if(request is string)
            {
                string validateString = (string)request;

                if (this.IsPotentialSqlInjectionAttack(validateString))
                {
                    this.ErrorList.Add($"{this.Name} contains illegal characters.");
                    this.stopValidation = true;
                }
            }
        }

        private bool IsPotentialSqlInjectionAttack(string data)
        {
            // Check to see whether the data contains illegal character
            // or the string for making comment such as "--" or "/*"
            char[] illegalChars = { ';', '\'', '\\', '"', '=', '%', '_', '*' };
            if ((data.IndexOfAny(illegalChars) != -1) || data.Contains("--") || data.Contains("/*"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
