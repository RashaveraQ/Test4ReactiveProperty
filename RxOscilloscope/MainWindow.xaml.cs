using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ReactiveProperty<string> PathData { get; }

        const int maxValue = 400;
        const int sampling_size = 100;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var random = new Random(Environment.TickCount);

            var source = Observable.Interval(TimeSpan.FromMilliseconds(20))
                            .Select(_ => random.Next(maxValue));

            var queue = source
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
    }
}
