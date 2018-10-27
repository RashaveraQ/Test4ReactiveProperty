using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class Person4View : Person
    {
        public ReactiveProperty<bool> IsSelected { get; set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsChecked { get; set; } = new ReactiveProperty<bool>(false);

        public Person4View(Person person) : base(person) {}
    }

    class MainViewModel
    {
        public ReadOnlyReactiveCollection<Person4View> People { get; }

        MainModel _model = new MainModel();

        public MainViewModel()
        {
            People = _model.People.ToReadOnlyReactiveCollection(x => new Person4View(x));
        }
    }
}
