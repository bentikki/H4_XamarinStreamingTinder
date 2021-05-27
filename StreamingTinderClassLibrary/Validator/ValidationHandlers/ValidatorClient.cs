using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Validator.ValidationHandlers
{
    /// <summary>
    /// Used to control validation rules.
    /// </summary>
    class ValidatorClient
    {
        private List<IValidator> _validators;
        private string _name;
        private List<string> _errors = new List<string>();

        public string Name { get => this._name; }
        public List<string> Errors { get => this._errors; }

        public ValidatorClient(string name, List<IValidator> validators)
        {
            _name = name;
            this._validators = validators;
        }

        public void AddValidatorRule(IValidator validator)
        {
            this._validators.Add(validator);
        }

        /// <summary>
        /// The client runs through every validation rule - to populate error list.
        /// </summary>
        /// <param name="inputObjectToValidate"></param>
        public void Validate(object inputObjectToValidate)
        {
            foreach (IValidator validator in this._validators)
            {
                validator.Handle(inputObjectToValidate, this._name);
                this._errors.AddRange(validator.ErrorList);
                if (validator.StopValidation)
                    break;
            }
        }
    }

}
