using Reactive.Bindings;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;

namespace Test4ReactiveProperty
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ReactiveProperty<Point>[] Points { get; } = new ReactiveProperty<Point>[4];

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Point c = new Point(100, 100);
            const double r = 100;

            var source = Observable
                .Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1))
                .Select(i => i * 2 * Math.PI / 360);

            Points[0] = source
                .Select(th => new Point(c.X + r * Math.Cos(th), c.Y + r * Math.Sin(th)))
                .ToReactiveProperty();

            Points[1] = source
                .Select(x => 2 * x)
                .Select(th => new Point(c.X + r * Math.Cos(th), c.Y + r * Math.Sin(th)))
                .ToReactiveProperty();

            Points[2] = MousePoint
                .Delay(TimeSpan.FromSeconds(1))
                .ToReactiveProperty();

            Points[3] = Points[2]
                .Delay(TimeSpan.FromMilliseconds(100))
                .ToReactiveProperty();
        }

        private Subject<Point> MousePoint { get; } = new Subject<Point>();
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MousePoint.OnNext(e.GetPosition(this));
        }
    }
}
