using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.ViewModel.Chart
{
    public class BarChart
    {
        public string[] labels { get; set; }
        public BarDataset[] datasets { get; set; }
    }

    public class BarDataset
    {
        public string label { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int borderWidth { get; set; }
        public string stack { get; set; }
        public double[] data { get; set; }
    }
}
