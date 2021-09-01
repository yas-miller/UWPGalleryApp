﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class DeleteMedia : Page
    {
        public string Path { get; }

        public DeleteMedia()
        {
            this.InitializeComponent();
            this.DataContext = mediaCollection.collection;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void Cancel_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectionPage));
        }

        private async void Delete_Button_ClickAsync(object sender, RoutedEventArgs e)
        {   
            //Create message dialog and set contents
            var confirmation = new MessageDialog("Are you sure you want to delete these photos?");
            //Add commands and set their callbacks
            confirmation.Commands.Add(new UICommand("Yes, Delete Photos", new UICommandInvokedHandler(this.CommandInvokedHandlerDelete)));
            confirmation.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandlerCancel)));
            //set command that will be invoked by default & cancel
            confirmation.DefaultCommandIndex = 0;
            confirmation.CancelCommandIndex = 1;
            await confirmation.ShowAsync();

        }

        public void CommandInvokedHandlerDelete(IUICommand command)
        {
            foreach (var p in this.DeleteGrid.SelectedItems)
            {
                Debug.WriteLine(p);
                MediaCollection.DeletePhotoFromCollection(p);
            }
            MediaCollection.getInstance().collection.Clear();
            MediaCollection.getInstance().LoadAllPicturesAsync();
            this.Frame.Navigate(typeof(CollectionPage));

        }

        public void CommandInvokedHandlerCancel(IUICommand command)
        {
            Debug.WriteLine(command.Label);
        }

        private void Album_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AlbumPage));
        }

        private void Collection_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectionPage));
        }

        public void DeleteGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
