using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using StreamingTinderClassLibrary.Validator.ValidationHandlers;

namespace StreamingTinderClassLibrary.Validator.Validators
{
    class UniqueUserInputValidator : InputValidatorHandler
    {
        public async override void Handle(object request)
        {
            if (request is string validateString)
            {
                if (!string.IsNullOrEmpty(validateString) && !string.IsNullOrWhiteSpace(validateString))
                {
                    IUserService userService = UserServiceFactory.GetUserService();

                    IUser user = await userService.GetUserByEmailAsync(validateString);

                    if (user != null)
                    {
                        this.ErrorList.Add($"A user with {this.Name}: {validateString}, already exists.");
                    }
                }
            }
        }

    }
}
