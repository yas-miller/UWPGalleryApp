using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoLibraryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MediaViewPage : Page
    {
        public MediaViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Items = MediaCollection.getInstance().collection;
            flipView.SelectedIndex = Convert.ToInt32(e.Parameter);
        }

        public string TextContent { get; set; }

        private void mediaFailedHandler(object sender, ExceptionRoutedEventArgs e)
        {
            // get HRESULT from event args
            String hr = String.Empty;
            
            String token = "HRESULT - ";
            const int hrLength = 10;     // eg "0xFFFFFFFF"

            int tokenPos = e.ErrorMessage.IndexOf(token, StringComparison.Ordinal);
            if (tokenPos != -1)
            {
                hr = e.ErrorMessage.Substring(tokenPos + token.Length, hrLength);
            }

            // Handle media failed event appropriately

        }

        private void MediaElement_PartialMediaFailureDetected(object sender, ExceptionRoutedEventArgs e)
        {
            // get HRESULT from event args
            String hr = String.Empty;

            String token = "HRESULT - ";
            const int hrLength = 10;     // eg "0xFFFFFFFF"

            int tokenPos = e.ErrorMessage.IndexOf(token, StringComparison.Ordinal);
            if (tokenPos != -1)
            {
                hr = e.ErrorMessage.Substring(tokenPos + token.Length, hrLength);
            }

            // Handle media failed event appropriately

        }

        // Items for the flip view
        public ObservableCollection<Media> Items { get; set; }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectionPage));
        }
        private void Add_Photos_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectionPage));
        }
        private void Delete_Photos_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectionPage));
        }
        private void CancelSelectionBtn_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectionPage));
        }
    }
}
