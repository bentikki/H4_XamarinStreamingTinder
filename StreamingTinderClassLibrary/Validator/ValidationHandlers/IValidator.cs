using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Validator.ValidationHandlers
{
    public interface IValidator
    {
        List<string> ErrorList { get; }
        bool StopValidation { get; }
        Task Handle(object request, string name);
        Task Handle(object request);
    }
}
