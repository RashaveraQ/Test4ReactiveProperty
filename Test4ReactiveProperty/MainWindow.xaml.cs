using Reactive.Bindings;
using System;
using System.Reactive.Linq;
using System.Windows;

namespace Test4ReactiveProperty
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ReactiveProperty<double> PosX { get; } = new ReactiveProperty<double>();
        public ReactiveProperty<double> PosY { get; } = new ReactiveProperty<double>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1))
                .Select(i => i * 2 * Math.PI / 360)
                .Subscribe( th =>
                    {
                        Point c = new Point(100, 100);
                        const double r = 100;
                        PosX.Value = c.X + r * Math.Cos(th);
                        PosY.Value = c.Y + r * Math.Sin(th);
                    }
                );
        }
    }
}
