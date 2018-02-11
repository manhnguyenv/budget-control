using BC.ViewModel.Resultset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.ViewModel.Common
{
    public class ManagementCommonVM : BaseCommonVM
    {
        public ManagementCommonVM()
        {
            Resultset = new ManagementResultsetVM();
            IsOk = true;
        }

        public ManagementResultsetVM Resultset { get; set; }
        public int SelectedYear { get; set; }
    }
}
