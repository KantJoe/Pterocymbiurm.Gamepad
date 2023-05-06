using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pterocymbiurm.Gamepad.WpfUI.Views
{
    /// <summary>
    /// Histogram.xaml 的交互逻辑
    /// </summary>
    public partial class Histogram : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public double MaxYValue { get; set; }

        public Histogram()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    MaxColumnWidth=5,
                    ColumnPadding=1,
                }
            };

            Labels = new[]{ 
                "dwSize", "dwFlags", "dwXpos", "dwYpos",
                    "dwZpos", "dwRpos", "dwUpos", "dwVpos","dwButton",
                    "dwButtonNumber", "dwPOV","dwReserved1","dwReserved2" 
            };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
