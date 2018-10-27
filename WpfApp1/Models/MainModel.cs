using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

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
                .Subscribe(t => People.Add(new Person() { Age = (int)t, Height = 100 + 10 * t, Weight = 20 * t }));
        }
    }
}
