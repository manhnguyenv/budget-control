using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.ViewModel.Common
{
    public class BaseCommonVM
    {
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public Exception Ex { get; set; }
        public string Code { get; set; }
    }
}
