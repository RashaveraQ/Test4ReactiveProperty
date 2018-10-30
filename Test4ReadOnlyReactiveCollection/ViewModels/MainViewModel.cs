using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class Person4View : Person
    {
        public ReactiveProperty<bool> IsSelected { get; set; }
        public ReactiveProperty<bool> IsChecked { get; set; }

        public Person4View(Person person) : base(person) {}
    }

    class MainViewModel
    {
        public ReadOnlyReactiveCollection<Person4View> People { get; }

        MainModel _model = new MainModel();

        public MainViewModel()
        {
            var r = new System.Random(Environment.TickCount);

            People = _model.People.ToReadOnlyReactiveCollection(x => new Person4View(x) {
                IsSelected = new ReactiveProperty<bool>(r.Next() % 2 == 0),
                IsChecked = new ReactiveProperty<bool>(r.Next() % 2 == 0),
            });
        }
    }
}
