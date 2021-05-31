using StreamingTinderClassLibrary.Validator.ValidationHandlers;
using StreamingTinderClassLibrary.Validator.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Validator
{
    /// <summary>
    /// Validation rules used for account creation.
    /// </summary>
    public static class DefaultValidator
    {
        /// <summary>
        /// Used for validating emails when creating a new account with email.
        /// </summary>
        /// <param name="email">Value to validate.</param>
        /// <returns>Returns string array with errors.</returns>
        static public string[] ValidEmailCreateNew(string email)
        {
            return ValidEmailCreateNewAsync(email).Result;
        }

        static public async Task<string[]> ValidEmailCreateNewAsync(string email)
        {
            List<IValidator> emailRules = new List<IValidator>()
            {
                new NullInputValidator(),
                new NoSpaceInputValidator(),
                new MaxLengthInputValidator(250),
                new MinLengthInputValidator(5),
                new SqlInjectionInputValidator(),
                new UniqueUserInputValidator()
            };

            var emailValidator = new ValidatorClient("Email", emailRules);

            await emailValidator.Validate(email);

            return emailValidator.Errors.ToArray();
        }

        /// <summary>
        /// Used for validating emails when loggin in with email.
        /// </summary>
        /// <param name="email">Value to validate.</param>
        /// <returns>Returns string array with errors.</returns>
        static public string[] ValidEmailLogin(string email)
        {
            List<IValidator> emailRules = new List<IValidator>()
            {
                new NullInputValidator(),
                new NoSpaceInputValidator(),
                new MaxLengthInputValidator(250),
                new MinLengthInputValidator(5),
                new SqlInjectionInputValidator()
            };

            var emailValidator = new ValidatorClient("Email", emailRules);

            emailValidator.Validate(email);

            return emailValidator.Errors.ToArray();
        }

        /// <summary>
        /// Used for validating password.
        /// </summary>
        /// <param name="email">Value to validate.</param>
        /// <returns>Returns string array with errors.</returns>
        static public string[] ValidPassword(string password)
        {
            List<IValidator> passwordRules = new List<IValidator>()
            {
                new NullInputValidator(),
                new MaxLengthInputValidator(250),
                new MinLengthInputValidator(6),
                new PasswordRequirementInputValidator()
            };

            var validator = new ValidatorClient("Password", passwordRules);

            validator.Validate(password);

            return validator.Errors.ToArray();
        }

        /// <summary>
        /// Used for validating username.
        /// </summary>
        /// <param name="email">Value to validate.</param>
        /// <returns>Returns string array with errors.</returns>
        static public string[] ValidUsername(string username)
        {
            List<IValidator> usernameRules = new List<IValidator>()
            {
                new NullInputValidator(),
                new NoSpaceInputValidator(),
                new SqlInjectionInputValidator(),
                new MaxLengthInputValidator(75),
                new MinLengthInputValidator(5)
            };

            var validator = new ValidatorClient("Username", usernameRules);

            validator.Validate(username);

            return validator.Errors.ToArray();
        }
    }
}
