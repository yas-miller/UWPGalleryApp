using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using System.Diagnostics;

namespace PhotoLibraryApp
{
    public class Media
    {
        // Path of the picture file
        public string Path { get; set; }

        // BitmapImage to be used as the souce of the image control
        [field: NonSerialized]
        public BitmapImage ImageSource { get; set; }
    }
}