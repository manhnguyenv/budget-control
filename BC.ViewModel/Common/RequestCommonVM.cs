using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC.ViewModel.Register;
using BC.ViewModel.Resultset;

namespace BC.ViewModel.Common
{
    public class RequestCommonVM : BaseCommonVM
    {
        public RequestCommonVM()
        {
            Register = new RequestRegisterVM();
            Resultset = new RequestResultsetVM();
            IsOk = true;
        }
        public RequestRegisterVM Register { get; set; }
        public RequestResultsetVM Resultset { get; set; }
    }
}
