using ChemModel.Messeges;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ScottPlot;
using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;


namespace ChemModel.ViewModels
{
    public partial class GraphicsViewModel : ObservableObject, IRecipient<DataMessage>
    {
        [ObservableProperty]
        private TableData[]? data;
        private WpfPlot tempPlot;
        private WpfPlot vazPlot;
        [ObservableProperty]
        private string nearXTemp = "--";
        [ObservableProperty]
        private string nearYTemp = "--";
        [ObservableProperty]
        private string nearXVaz = "--";
        [ObservableProperty]
        private string nearYVaz = "--";
        [ObservableProperty]
        private ObservableCollection<ObservablePoint> tempPoints;
        [ObservableProperty]
        private ObservableCollection<ObservablePoint> viscosityPoints;
        [ObservableProperty]
        private IEnumerable<ISeries> seriesT;

        [ObservableProperty] private IEnumerable<ISeries> seriesV;

        public ScottPlot.Plottables.Scatter? ScatterTemp { get; set; }
        public ScottPlot.Plottables.Scatter? ScatterVaz { get; set; }
        public GraphicsViewModel()
        {
            TempPoints = new ObservableCollection<ObservablePoint>();
            ViscosityPoints = new ObservableCollection<ObservablePoint>();
            SeriesT = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservablePoint>()
                {
                    Values = tempPoints,
                    Fill = null
                }
            };
            SeriesV = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservablePoint>
                {
                    Values = viscosityPoints,
                    Fill = null
                }
            };
            WeakReferenceMessenger.Default.Register<DataMessage>(this);
            
        }
        [ObservableProperty]
        private Axis[] xAxesTemp  =
        {
            new Axis
            {
                Name = "Координата по длине канала (м)",
                LabelsPaint = new SolidColorPaint(SKColors.Black),
                Labeler = value => value.ToString("N2")
            }
        };
        [ObservableProperty]
        private Axis[] yAxesTemp  =
        {
            new Axis
            {
                Name = "Температура (°С)",

            }
        };
        [ObservableProperty]
        private Axis[] xAxesV  =
        {
            new Axis
            {
                Name = "Координата по длине канала (м)",
                LabelsPaint = new SolidColorPaint(SKColors.Black),
                Labeler = value => value.ToString("N2")
            }
        };
        [ObservableProperty]
        private Axis[] yAxesV  =
        {
            new Axis
            {
                Name = "Вязкость материала (Па*с)",

            }
        };

        public void Receive(DataMessage message)
        {
          
            Data = message.Value;
            double[] xs = Data.Select(x => x.Coord).ToArray();
            double[] temp = Data.Select(x => x.Temp).ToArray();
            double[] vaz = Data.Select(x => x.Vaz).ToArray();
            for (int i = 0; i < xs.Length; i++)
            {
                tempPoints.Add(new(xs[i], Math.Round(temp[i],3)));
                viscosityPoints.Add(new(xs[i], Math.Round(vaz[i],3)));

            }
           
        }
    }
}
