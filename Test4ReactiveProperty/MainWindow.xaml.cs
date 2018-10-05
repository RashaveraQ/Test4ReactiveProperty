using Reactive.Bindings;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Test4ReactiveProperty
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ReactiveProperty<double> PosX { get; } = new ReactiveProperty<double>(100);
        public ReactiveProperty<double> PosY { get; } = new ReactiveProperty<double>(20);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Task.Run(() => {
                Point c = new Point(100,100);
                const double r = 100;
                for (double th = 0; ; th += 0.01) {
                    PosX.Value = c.X + r * Math.Cos(th);
                    PosY.Value = c.Y + r * Math.Sin(th);
                    Thread.Sleep(10);
                }
            });
        }
    }
}
