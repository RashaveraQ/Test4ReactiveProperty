using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models
{
    public class Offering : INotifyPropertyChanged
    {
        public string? Title { get; set; }

        bool isAlreadyRead;
        public bool IsAlreadyRead
        {
            get => isAlreadyRead;
            set {
                isAlreadyRead = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? caller = null) =>
            PropertyChanged?.Invoke(this, new (caller));
    }
}
