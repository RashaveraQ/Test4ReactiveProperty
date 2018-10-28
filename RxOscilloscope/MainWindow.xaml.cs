using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ReactiveProperty<string> PathData { get; }
        Subject<MouseEventArgs> subject = new Subject<MouseEventArgs>();

        Point mousePos;

        const int maxValue = 450;
        const int sampling_size = 100;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //var random = new Random(Environment.TickCount);
            var source = Observable.Interval(TimeSpan.FromMilliseconds(1))
                           .Select(_ => (int)mousePos.Y);

            var delay_input = new ReactiveProperty<int>();

            var delay_output = delay_input
                                    .Delay(TimeSpan.FromSeconds(1))
                                    .ToReactiveProperty();

            source.CombineLatest(delay_output, (x, y) => (int)(x + 0.5 * (y - maxValue / 2)))
                .Subscribe(_ => delay_input.Value = _);

            var queue = delay_output 
                            .Scan(new List<int>(), (q, x) => {
                                q.Add(x);
                                while (q.Count > sampling_size) q.RemoveAt(0);
                                return q;
                            })
                            .SelectMany(x => x);

            PathData = Observable.Range(0, sampling_size)
                        .Zip(queue, (x, y) => string.Format("{0} {1},{2} ", x == 0 ? "M" : "L", 10 * x, y))
                        .Aggregate((x, y) => x + y)
                        .Repeat()
                        .ToReactiveProperty();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e.GetPosition(Canvas);
        }
    }
}
