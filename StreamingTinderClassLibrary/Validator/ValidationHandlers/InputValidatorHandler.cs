using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Validator.ValidationHandlers
{
    abstract class InputValidatorHandler : IValidator
    {
        protected bool stopValidation = false;

        public string Name { get; protected set; }
        public List<string> ErrorList { get; protected set; } = new List<string>();
        public bool StopValidation { get => this.stopValidation; }

        public Task Handle(object request, string name) 
        {
            this.Name = name;
            return this.Handle(request);
        }

        public abstract Task Handle(object request);
    }
}
