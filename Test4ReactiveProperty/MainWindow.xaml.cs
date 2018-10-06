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
        public ReactiveProperty<double> PosX { get; } = new ReactiveProperty<double>(100);
        public ReactiveProperty<double> PosY { get; } = new ReactiveProperty<double>(20);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1))
                .Subscribe( i =>
                    {
                        Point c = new Point(100, 100);
                        const double r = 100;
                        double th = i * 2 * Math.PI / 360;
                        PosX.Value = c.X + r * Math.Cos(th);
                        PosY.Value = c.Y + r * Math.Sin(th);
                    }
                );
        }
    }
}
