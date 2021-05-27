using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Validator.ValidationHandlers
{
    abstract class InputValidatorHandler : IValidator
    {
        protected bool stopValidation = false;

        public string Name { get; protected set; }
        public List<string> ErrorList { get; protected set; } = new List<string>();
        public bool StopValidation { get => this.stopValidation; }

        public void Handle(object request, string name) 
        {
            this.Name = name;
            this.Handle(request); 
        }

        public abstract void Handle(object request);
    }
}
