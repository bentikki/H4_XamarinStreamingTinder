using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Validator.ValidationHandlers
{
    public interface IValidator
    {
        List<string> ErrorList { get; }
        bool StopValidation { get; }
        void Handle(object request, string name);
        void Handle(object request);
    }
}
