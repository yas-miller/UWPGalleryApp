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
    public class MediaCollection
    {
        private static MediaCollection instance;
        private static object semaphore = new Object();
        public static MediaCollection getInstance()
        {
            if (instance == null)
            {
                lock (semaphore)
                {
                    if (instance == null)
                        instance = new MediaCollection();
                }
            }
            return instance;
        }


        public LocalXMLStorage localXMLStorage = new LocalXMLStorage();

        // Global collection of pictures
        public ObservableCollection<Media> collection = new ObservableCollection<Media>();

        public StorageFile mainStorageFile;

        /// <summary>
        /// Adds pictures to the library and updates storage file
        /// </summary>
        /// <param name="picture"></param>
        public async Task AddMedia(IReadOnlyList<StorageFile> storageFiles)
        {
            foreach (var storageFile in storageFiles)
            {
                mainStorageFile = storageFile;
                if (collection.Any(p => p.Path == storageFile.Path) == false)
                {


                    // Add picture to the global collection
                    await AddPictureToCollection(storageFile.Path);

                    // ToDo Save picture file path in storage data file
                    //localXMLStorage.AppendToXmlFile(storageFile.Path);
                }
                else
                {
                    var dialog = new MessageDialog($"The file '{storageFile.Path}' already exists in the collection.");
                    await dialog.ShowAsync();
                }
            }

            localXMLStorage.writeXmlFile(collection);
        }

        /// <summary>
        /// Adds pictures to the library and updates storage file
        /// </summary>
        /// <param name="picture"></param>
        public async Task AddMedia(StorageFolder storageFolder, Windows.Storage.Search.CommonFileQuery fileQuery)
        {
            var folderFiles = storageFolder.GetFilesAsync(fileQuery);

            foreach (var storageFile in await folderFiles)
            {
                mainStorageFile = storageFile;
                if (collection.Any(p => p.Path == storageFile.Path) == false)
                {

                    // Add picture to the global collection
                    await AddPictureToCollection(storageFile.Path);

                    // Save picture file path in storage data file
                    //LocalXMLStorage.WriteXmlFile( File.WriteTextFileAsync(TEXT_FILE_NAME, storageFile.Path);
                }
                else
                {
                    var dialog = new MessageDialog($"The file '{storageFile.Path}' already exists in the collection.");
                    await dialog.ShowAsync();
                }
            }

            localXMLStorage.writeXmlFile(collection);
        }

        /// <summary>
        /// Loads all pictures from the library data file
        /// </summary>
        /// <returns>A list of pictures</returns>
        public async Task LoadAllPicturesAsync()
        {
            var mediaCollection = localXMLStorage.readXmlFile<Collection<Media>>();

            foreach (var file in mediaCollection)
            {
                await AddPictureToCollection(file.Path);
            }
        }

        //Delete Photos Method: 
        public async Task DeletePhotoFromCollection(string path)
        {
            
        }

        private async Task AddPictureToCollection(string filePath)
        {
            // Create a bitmap
            StorageFile storageFile = null;

            try
            {
                storageFile = await StorageFile.GetFileFromPathAsync(filePath);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Access denied to the folder");
            }

            BitmapImage bitmapImage = new BitmapImage();
            var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            bitmapImage.SetSource(stream);

            // Create Picture object
            var pic = new Media() { Path = storageFile.Path, ImageSource = bitmapImage };

            // Add Picture object to the global observable collection
            collection.Add(pic);
        }
    }
}