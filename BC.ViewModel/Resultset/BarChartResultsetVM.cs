using BC.ViewModel.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.ViewModel.Resultset
{
    public class BarChartResultsetVM
    {
        public BarChartResultsetVM()
        {
            configuration = new BaseChart();
            data = new BarChart();
        }

        public BaseChart configuration { get; set; }
        public BarChart data { get; set; }
    }
}
