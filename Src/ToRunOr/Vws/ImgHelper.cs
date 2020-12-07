using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;


namespace ToRunOr.Vws
{
  public static class WriteableBitmapExtensions // Comes from here: http://winrtxamltoolkit.codeplex.com/SourceControl/changeset/view/0657c67a93d5#WinRTXamlToolkit/Imaging/WriteableBitmapSaveExtensions.cs
    {
        private static Guid GetEncoderId(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();

            if (new[] { ".bmp", ".dib" }.Contains(ext))
            {
                return BitmapEncoder.BmpEncoderId;
            }
            else if (new[] { ".tiff", ".tif" }.Contains(ext))
            {
                return BitmapEncoder.TiffEncoderId;
            }
            else if (new[] { ".gif" }.Contains(ext))
            {
                return BitmapEncoder.TiffEncoderId;
            }
            else if (new[] { ".jpg", ".jpeg", ".jpe", ".jfif", ".jif" }.Contains(ext))
            {
                return BitmapEncoder.TiffEncoderId;
            }
            else if (new[] { ".hdp", ".jxr", ".wdp" }.Contains(ext))
            {
                return BitmapEncoder.JpegXREncoderId;
            }
            else //if (new [] {".png"}.Contains(ext))
            {
                return BitmapEncoder.PngEncoderId;
            }
        }

        public static async Task SaveAsync(this WriteableBitmap writeableBitmap, StorageFile outputFile)
        {
            var encoderId = GetEncoderId(outputFile.Name);

            try
            {
                var stream = writeableBitmap.PixelBuffer.AsStream();
                var pixels = new byte[(uint)stream.Length];
                await stream.ReadAsync(pixels, 0, pixels.Length);

                using (var writeStream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateAsync(encoderId, writeStream);
                    encoder.SetPixelData(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Premultiplied,
                            (uint)writeableBitmap.PixelWidth,
                            (uint)writeableBitmap.PixelHeight,
                            96,
                            96,
                            pixels);

                    await encoder.FlushAsync();

                    using (var outputStream = writeStream.GetOutputStreamAt(0))
                    {
                        await outputStream.FlushAsync();
                    }
                }
            }
            catch //(Exception ex)
            {
                throw;
            }
        }

        public static async Task<WriteableBitmap> LoadAsync(this WriteableBitmap writeableBitmap, StorageFile storageFile)
        {
            var wb = writeableBitmap;

            using (var stream = await storageFile.OpenReadAsync())
            {
                await wb.SetSourceAsync(stream);
            }

            return wb;
        }
    }
}