using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CollectionsProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {        
        ObservableCollection<Person> obData;

        public MainPage()
        {
            this.InitializeComponent();

            Person[] data = new Person[] {
                new Person{Nome="Antonio", Cognome="Cisternino"},
                new Person{Nome="Davide", Cognome="Patrizi"}
            };

            obData = new ObservableCollection<Person>(data);
            Persone.ItemsSource = obData;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var f = Windows.Storage.ApplicationData.Current.LocalFolder;
            Persone.ItemsSource = await f.GetFilesAsync();

            obData.Add(new Person { Nome = "AAA", Cognome = "AAAA" });
            obData[1].Cognome = "Gervasi";
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private string nome;

        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged();
            }
        }

        private string cognome;

        public string Cognome
        {
            get { return cognome; }
            set
            {
                cognome = value;
                OnPropertyChanged();
            }
        }

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}