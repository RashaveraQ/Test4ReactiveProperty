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
        public ReactiveProperty<Point>[] Points { get; } = {
            new ReactiveProperty<Point>(),
            new ReactiveProperty<Point>()
        };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Point c = new Point(100, 100);
            const double r = 100;

            var source = Observable
                .Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1))
                .Select(i => i * 2 * Math.PI / 360);
           
            source
                .Subscribe( th => Points[0].Value = new Point(c.X + r * Math.Cos(th), c.Y + r * Math.Sin(th)));

            source
                .Select(x => 2 * x)
                .Subscribe(th => Points[1].Value = new Point(c.X + r * Math.Cos(th), c.Y + r * Math.Sin(th)));
        }
    }
}
