using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class LocalFolderNav : Page
    {
        public static DependencyProperty CurrentFolderFQNProperty = DependencyProperty.Register("CurrentFolderFQN", typeof(String), typeof(LocalFolderNav), null);

        public string CurrentFolderFQN
        {
            get { return (string)GetValue(CurrentFolderFQNProperty); }
            set { SetValue(CurrentFolderFQNProperty, value); }
        }

        ObservableCollection<StorageFolder> folders;
        StorageFolder currentFolder;

        public LocalFolderNav()
        {
            this.InitializeComponent();
            currentFolder = ApplicationData.Current.LocalFolder;
            CurrentFolderFQN = currentFolder.Path + "/" + currentFolder.Name;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            folders = new ObservableCollection<StorageFolder>(await currentFolder.GetFoldersAsync());
            CurrentFolderBox.ItemsSource = folders;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await ApplicationData.Current.LocalFolder.GetFolderAsync("Prova3") == null)
                {
                    var s = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Prova3");
                    folders.Add(s);
                }
            }
            catch { }
        }

        private async void TextBlock_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var tb = sender as TextBlock;
            var f = tb.DataContext as StorageFolder;
            folders = new ObservableCollection<StorageFolder>(await f.GetFoldersAsync());
            currentFolder = f;
            CurrentFolderFQN = currentFolder.Path + "/" + f.Name;
            CurrentFolderBox.ItemsSource = folders;
        }
    }
}