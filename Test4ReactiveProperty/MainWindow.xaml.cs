using Reactive.Bindings;
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
        }
    }
}
