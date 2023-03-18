using Reactive.Bindings;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainViewModel
    {
        public ReactiveCollection<Offering> Offerings { get; set; }

        public ReactiveCommand<Offering> ClickCommand { get; set; }

        public MainViewModel()
        {
            Offerings = new ReactiveCollection<Offering>();

            Offerings.AddRangeOnScheduler(new Offering[] {
                new Offering() { Title = "hoge", IsAlreadyRead = false},
                new Offering() { Title = "fuga", IsAlreadyRead = false},
                new Offering() { Title = "piyo", IsAlreadyRead = true},
                new Offering() { Title = "hoge2", IsAlreadyRead = false},
            });

            ClickCommand = new ReactiveCommand<Offering>()
                .WithSubscribe( offering => offering.IsAlreadyRead = true);
        }
    }
}
