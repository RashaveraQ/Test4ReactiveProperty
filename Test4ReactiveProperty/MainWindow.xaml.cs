using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Test4ReactiveProperty
{
    //public class Person : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
    //    {
    //        if (object.Equals(field, value)) { return; }
    //        field = value;
    //        var h = this.PropertyChanged;
    //        if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
    //    }

    //    private string name;
    //    public string Name
    //    {
    //        get { return name; }
    //        set { SetProperty(ref name, value); }
    //    }
    //}

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ReactiveProperty<Point>[] Points { get; } = new ReactiveProperty<Point>[4];
        public ReactiveProperty<bool> bChecked1 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> bChecked2 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> bChecked3 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> bChecked4 { get; } = new ReactiveProperty<bool>();

        public ReadOnlyReactiveCollection<int> cc { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Point c = new Point(100, 100);
            const double r = 100;

            //cc = Observable.Merge(
            //    new ReactiveCommand().Select(_ => CollectionChanged<int>.Add(0, 2)),
            //    new ReactiveCommand().Select(_ => CollectionChanged<int>.Reset)
            //    )
            //    .ToReadOnlyReactiveCollection();

            new ObservableCollection<int> { 1, 2, 34, 5, 6, 7, 8 }
                .ToReadOnlyReactiveCollection(x => x * 2)
                .ToObservable()
                .Subscribe(_ => Console.WriteLine(_));
            
            new int[] { 1, 5, 3, 5, 7, 8, 3, 2, 7 }
                .ToObservable()
                .Subscribe(i => Console.WriteLine(i));

            var source = Observable
                .Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1)/*, Scheduler.NewThread*/)
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

            bChecked1.Subscribe(b => bChecked2.Value = !b);
            bChecked2.Subscribe(b => bChecked3.Value = !b);
            bChecked3.Subscribe(b => bChecked4.Value = !b);
            bChecked4.Subscribe(b => bChecked1.Value = !b);
        }

        private Subject<Point> MousePoint { get; } = new Subject<Point>();
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MousePoint.OnNext(e.GetPosition(this));
        }
    }
}
