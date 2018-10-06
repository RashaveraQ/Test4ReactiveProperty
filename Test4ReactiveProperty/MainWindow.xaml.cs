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
            new ReactiveProperty<Point>(),
            new ReactiveProperty<Point>(),
            new ReactiveProperty<Point>(),
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

            MousePoint
                .Delay(TimeSpan.FromSeconds(1))
                .Subscribe(p => Points[2].Value = p);

            Points[2]
                .Delay(TimeSpan.FromMilliseconds(100))
                .Subscribe(p => Points[3].Value = p);
        }

        private ReactiveProperty<Point> MousePoint { get; } = new ReactiveProperty<Point>();
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MousePoint.Value = e.GetPosition(this);
        }
    }
}
