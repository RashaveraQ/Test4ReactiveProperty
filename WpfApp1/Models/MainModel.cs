using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp1.Models
{

    class MainModel
    {
        public ObservableCollection<Person> People { get; private set; } = new ObservableCollection<Person>(
            new Person[] {
                new Person() {
                    Name = "Alice",
                    Age = 10,
                    Height = 170,
                    Weight = 30
               },
                new Person() {
                    Name = "Bob",
                    Age = 13,
                    Height = 150,
                    Weight = 45
               },
            });

        public MainModel()
        {
            Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
                .ObserveOn(Application.Current.Dispatcher)
                .Subscribe(t => People.Add(new Person() { Age = (int)t }));
        }
    }
}
