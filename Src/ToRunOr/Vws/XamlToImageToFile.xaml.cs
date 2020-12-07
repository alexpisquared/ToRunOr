using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ToRunOr.Vws
{
    public sealed partial class XamlToImageToFile : Page
    {
        public XamlToImageToFile() { this.InitializeComponent(); }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
        }

        async void onXamlToImage(object sender, RoutedEventArgs e)
        {


            Grid grid = new Grid { Width = 100, Height = 80, Background = new SolidColorBrush(Colors.Red) };
            //grid.Children.Add(new ucAnalogClock());

            var rtb = new RenderTargetBitmap(); // https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.media.imaging.rendertargetbitmap.aspx?f=255&MSPPError=-2147217396
                                                //nogo: await rtb.RenderAsync(grid);
            await rtb.RenderAsync(panel4lockscreen);
            imageFromXaml.Source = rtb;




            var buffer = await rtb.GetPixelsAsync();
            var bitmap = Windows.Graphics.Imaging.SoftwareBitmap.CreateCopyFromBuffer(buffer, BitmapPixelFormat.Bgra8, rtb.PixelWidth, rtb.PixelHeight, BitmapAlphaMode.Premultiplied);



            string filename = "ScreenCapture.jpg";

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await SaveSoftwareBitmapToFile(bitmap, file);


            imageFromFile.Source = await LoadSoftwareBitmapFromFile(filename);


            var wb = new WriteableBitmap(555, 555);
            wb.Invalidate();


        }
        async Task<ImageSource> LoadSoftwareBitmapFromFile(string filename)
        {
            var imgFile = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            if (imgFile == null)
                return null;

            var wb = new WriteableBitmap(1, 1);
            await wb.LoadAsync(imgFile);
            return wb;
        }

        private void OnGoHome(object sender, RoutedEventArgs e) { (Window.Current.Content as Frame).Navigate(typeof(MainPage)); }





        SoftwareBitmap _softwareBitmap;


        async Task CreateEditSaveBitmapImages()
        {
            //Create a SoftwareBitmap from an image file with BitmapDecoder
            //To create a SoftwareBitmap from a file, get an instance of StorageFile containing the image data. This example uses a FileOpenPicker to allow the user to select an image file.

            FileOpenPicker fileOpenPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;
            var inputFile = await fileOpenPicker.PickSingleFileAsync();
            if (inputFile == null)
            {
                return;       // The user cancelled the picking operation
            }


            //Call the OpenAsync method of the StorageFile object to get a random access stream containing the image data.Call the static method BitmapDecoder.CreateAsync to get an instance of the BitmapDecoder class for the specified stream.Call GetSoftwareBitmapAsync to get a SoftwareBitmap object containing the image.

            using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream); // Create the decoder from the stream

                _softwareBitmap = await decoder.GetSoftwareBitmapAsync(); // Get the SoftwareBitmap representation of the file
            }


            //			Save a SoftwareBitmap to a file with BitmapEncoder
            //To save a SoftwareBitmap to a file, get an instance of StorageFile to which the image will be saved.This example uses a FileSavePicker to allow the user to select an output file.

            FileSavePicker fileSavePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            fileSavePicker.FileTypeChoices.Add("JPEG files", new List<string>() { ".jpg" });
            fileSavePicker.SuggestedFileName = "image";

            var outputFile = await fileSavePicker.PickSaveFileAsync();
            if (outputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }
        }
        //Call the OpenAsync method of the StorageFile object to get a random access stream to which the image will be written.Call the static method BitmapEncoder.CreateAsync to get an instance of the BitmapEncoder class for the specified stream.The first parameter to CreateAsync is a GUID representing the codec that should be used to encode the image.BitmapEncoder class exposes a property containing the ID for each codec supported by the encoder, such as JpegEncoderId.

        //Use the SetSoftwareBitmap method to set the image that will be encoded.You can set values of the BitmapTransform property to apply basic transforms to the image while it is being encoded. The IsThumbnailGenerated property determines whether a thumbnail is generated by the encoder. Note that not all file formats support thumbnails, so if you use this feature, you should catch the unsupported operation error that will be thrown if thumbnails are not supported.



        //Call FlushAsync to cause the encoder to write the image data to the specified file.

        private async Task SaveSoftwareBitmapToFile(SoftwareBitmap softwareBitmap, StorageFile outputFile)
        {
            using (IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream); // Create an encoder with the desired format

                encoder.SetSoftwareBitmap(softwareBitmap); // Set the software bitmap

                // Set additional encoding parameters, if needed
                encoder.BitmapTransform.ScaledWidth = 320;
                encoder.BitmapTransform.ScaledHeight = 240;
                encoder.BitmapTransform.Rotation = Windows.Graphics.Imaging.BitmapRotation.Clockwise90Degrees;
                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Fant;
                encoder.IsThumbnailGenerated = true;

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err)
                {
                    switch (err.HResult)
                    {
                        case unchecked((int)0x88982F81): //WINCODEC_ERR_UNSUPPORTEDOPERATION
                                                         // If the encoder does not support writing a thumbnail, then try again
                                                         // but disable thumbnail generation.
                            encoder.IsThumbnailGenerated = false;
                            break;
                        default:
                            throw err;
                    }
                }

                if (encoder.IsThumbnailGenerated == false)
                {
                    await encoder.FlushAsync();
                }


                //You can specify additional encoding options when you create the BitmapEncoder by creating a new BitmapPropertySet object and populating it with one or more BitmapTypedValue objects representing the encoder settings.For a list of supported encoder options, see BitmapEncoder options reference.


                var propertySet = new Windows.Graphics.Imaging.BitmapPropertySet();
                var qualityValue = new Windows.Graphics.Imaging.BitmapTypedValue(
                        1.0, // Maximum quality
                        Windows.Foundation.PropertyType.Single);

                propertySet.Add("ImageQuality", qualityValue);

                await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.JpegEncoderId, stream, propertySet);



            }







            //Use SoftwareBitmap with a XAML Image control

            //To display an image within a XAML page using the Image control, first define an Image control in your XAML page: <Image x:Name="imageFromXaml"/>
            //Currently, the Image control only supports images that use BGRA8 encoding and pre-multiplied or no alpha channel.Before attempting to display an image, test to make sure it has the correct format, and if not, use the SoftwareBitmap static Convert method to convert the image to the supported format.
            //Create a new SoftwareBitmapSource object. Set the contents of the source object by calling SetBitmapAsync, passing in a SoftwareBitmap.Then you can set the Source property of the Image control to the newly created SoftwareBitmapSource.

            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 || softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            var source = new SoftwareBitmapSource();
            await source.SetBitmapAsync(softwareBitmap);

            // Set the source of the Image control
            imageFromXaml.Source = source;



            //You can also use SoftwareBitmapSource to set a SoftwareBitmap as the ImageSource for an ImageBrush.

            //Create a SoftwareBitmap from a WriteableBitmap

            //You can create a SoftwareBitmap from an existing WriteableBitmap by calling SoftwareBitmap.CreateCopyFromBuffer and supplying the PixelBuffer property of the WriteableBitmap to set the pixel data. The second argument allows you to request a pixel format for the newly created WriteableBitmap. You can use the PixelWidth and PixelHeight properties of the WriteableBitmap to specify the dimensions of the new image.

            var rtb = new RenderTargetBitmap();// (ImageSource)imageFromXaml.Source;
            await rtb.RenderAsync(panel4lockscreen);

            WriteableBitmap writeableBitmap = new WriteableBitmap(rtb.PixelWidth, rtb.PixelHeight);
            SoftwareBitmap outputBitmap = SoftwareBitmap.CreateCopyFromBuffer(
                    writeableBitmap.PixelBuffer,
                    BitmapPixelFormat.Bgra8,
                    writeableBitmap.PixelWidth,
                    writeableBitmap.PixelHeight
            );
        }









        ////	Create or edit a SoftwareBitmap programmatically

        ////So far this topic has addressed working with image files.You can also create a new SoftwareBitmap programatically in code and use the same technique to access and modify the SoftwareBitmap's pixel data.

        ////SoftwareBitmap uses COM interop to expose the raw buffer containing the pixel data.

        ////To use COM interop, you must include a reference to the System.Runtime.InteropServices namespace in your project.

        ////using System.Runtime.InteropServices;
        ////Initialize the IMemoryBufferByteAccess COM interface by adding the following code within your namespace.

        //[ComImport]
        //[Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
        //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        //unsafe interface IMemoryBufferByteAccess
        //{
        //	void GetBuffer(out byte* buffer, out uint capacity);
        //}
        ////Create a new SoftwareBitmap with pixel format and size you want.Or, use an existing SoftwareBitmap for which you want to edit the pixel data. Call SoftwareBitmap.LockBuffer to obtain an instance of the BitmapBuffer class representing the pixel data buffer.Cast the BitmapBuffer to the IMemoryBufferByteAccess COM interface and then call IMemoryBufferByteAccess.GetBuffer to populate a byte array with data.Use the BitmapBuffer.GetPlaneDescription method to get a BitmapPlaneDescription object that will help you calculate the offset into the buffer for each pixel.

        //void iii()
        //{
        //	_softwareBitmap = new SoftwareBitmap(BitmapPixelFormat.Bgra8, 100, 100);

        //	using (BitmapBuffer buffer = _softwareBitmap.LockBuffer(BitmapBufferAccessMode.Write))
        //	{
        //		using (var reference = buffer.CreateReference())
        //		{
        //			byte* dataInBytes;
        //			uint capacity;
        //			((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacity);

        //			// Fill-in the BGRA plane
        //			BitmapPlaneDescription bufferLayout = buffer.GetPlaneDescription(0);
        //			for (int i = 0; i < bufferLayout.Height; i++)
        //			{
        //				for (int j = 0; j < bufferLayout.Width; j++)
        //				{

        //					byte value = (byte)((float)j / bufferLayout.Width * 255);
        //					dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 0] = value;
        //					dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 1] = value;
        //					dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 2] = value;
        //					dataInBytes[bufferLayout.StartIndex + bufferLayout.Stride * i + 4 * j + 3] = (byte)255;
        //				}
        //			}
        //		}
        //	}
        //}
        ////Because this method accesses the raw buffer underlying the Windows Runtime types, it must be declared using the unsafe keyword.You must also configure your project in Microsoft Visual Studio to allow the compilation of unsafe code by opening the project's Properties page, clicking the Build property page, and selecting the Allow Unsafe Code checkbox.

        ////Create a SoftwareBitmap from a Direct3D surface

        ////To create a SoftwareBitmap object from a Direct3D surface, you must include the Windows.Graphics.DirectX.Direct3D11 namespace in your project.

        ////using Windows.Graphics.DirectX.Direct3D11;
        ////Call CreateCopyFromSurfaceAsync to create a new SoftwareBitmap from the surface.As the name indicates, the new SoftwareBitmap has a separate copy of the image data.Modifications to the SoftwareBitmap will not have any effect on the Direct3D surface.

        //private async void CreateSoftwareBitmapFromSurface(IDirect3DSurface surface)
        //{
        //	_softwareBitmap = await SoftwareBitmap.CreateCopyFromSurfaceAsync(surface);
        //}
        ////Convert a SoftwareBitmap to a different pixel format

        ////The SoftwareBitmap class provides the static method, Convert, that allows you to easily create a new SoftwareBitmap that uses the pixel format and alpha mode you specify from an existing SoftwareBitmap.Note that the newly created bitmap has a separate copy of the image data.Modifications to the new bitmap will not affect the source bitmap.
        //void fff()
        //{
        //	SoftwareBitmap bitmapBGRA8 = SoftwareBitmap.Convert(_softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
        //}


        //Transcode an image file

        //You can transcode an image file directly from a BitmapDecoder to a BitmapEncoder.Create a IRandomAccessStream from the file to be transcoded.Create a new BitmapDecoder from the input stream.Create a new InMemoryRandomAccessStream for the encoder to write to and call BitmapEncoder.CreateForTranscodingAsync, passing in the in-memory stream and the decoder object. Set the encoding properties you want. Any properties in the input image file that you do not specifically set on the encoder, will be written to the output file unchanged. Call FlushAsync to cause the encoder to encode to the in-memory stream. Finally, seek the file stream and the in-memory stream to the beginning and call CopyAsync to write the in-memory stream out to the file stream.

        private async void TranscodeImageFile(StorageFile imageFile)
        {
            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

                var memStream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(memStream, decoder);

                encoder.BitmapTransform.ScaledWidth = 320;
                encoder.BitmapTransform.ScaledHeight = 240;

                await encoder.FlushAsync();

                memStream.Seek(0);
                fileStream.Seek(0);
                fileStream.Size = 0;
                await RandomAccessStream.CopyAsync(memStream, fileStream);

                memStream.Dispose();
            }
        }










        /// <summary>
        /// https://msdn.microsoft.com/en-us/windows/uwp/audio-video-camera/basic-photo-video-and-audio-capture-with-mediacapture?f=255&MSPPError=-2147217396
        /// </summary>
        async void img2file()
        {
            MediaCapture _mediaCapture;
            //bool _isPreviewing;
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();
            //todo: _mediaCapture.Failed += MediaCapture_Failed;


            var myPictures = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);
            StorageFile file = await myPictures.SaveFolder.CreateFileAsync("photo.jpg", CreationCollisionOption.GenerateUniqueName);

            using (var captureStream = new InMemoryRandomAccessStream())
            {
                await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), captureStream);

                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var decoder = await BitmapDecoder.CreateAsync(captureStream);
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(fileStream, decoder);

                    var properties = new BitmapPropertySet { { "System.Photo.Orientation", new BitmapTypedValue(PhotoOrientation.Normal, PropertyType.UInt16) } };

                    await encoder.BitmapProperties.SetPropertiesAsync(properties);
                    await encoder.FlushAsync();
                }
            }
        }

    }
}
/// xaml to image:
/// http://metulev.com/render-xaml-to-image-and-more/
/// signature for timetracker:
/// https://blogs.windows.com/buildingapps/2015/09/08/going-beyond-keyboard-mouse-and-touch-with-natural-input-10-by-10/#EHSzzHHR3zrgeXRB.97
/// file io
/// https://msdn.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files
/// image io:
/// https://msdn.microsoft.com/en-us/windows/uwp/audio-video-camera/imaging?f=255&MSPPError=-2147217396
/// 


///
/// http://codezero.one/Details?d=1592&a=9&f=181&l=0&v=d&t=UWP:-Save-a-BitmapImage--as-File
/// 
/// 
/// 
/// 
/// 
/// 
/// http://stackoverflow.com/questions/37746793/uwp-how-to-use-writeablebimap-in-a-background-task-alternative
/// 