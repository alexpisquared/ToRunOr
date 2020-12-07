using System;

namespace ToRunOr.Vws
{
	public class UwpIO
	{
		async void ff(string filename= "Sample.txt")
		{
			//		Creating a file
			//Here's how to create a file in the app's local folder.If it already exists, we replace it.
			// Create sample file; replace if exists.
			Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
			Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.ReplaceExisting);

			//		Writing to a file
			//Here's how to write to a writable file on disk using the StorageFile class. The common first step for each of the ways of writing to a file (unless you're writing to the file immediately after creating it) is to get the file with StorageFolder.GetFileAsync.

			/*Windows.Storage.StorageFolder*/
			storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
			/*Windows.Storage.StorageFile*/
			sampleFile = await storageFolder.GetFileAsync(filename);

			//		Writing text to a file
			//		Write text to your file by calling the WriteTextAsync method of the FileIO class.
			await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Swift as a shadow");

			//		Writing bytes to a file by using a buffer(2 step2)
			//First, call ConvertStringToBinary to get a buffer of the bytes(based on an arbitrary string) that you want to write to your file.
			var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary("What fools these mortals be", Windows.Security.Cryptography.BinaryStringEncoding.Utf8);

			//	Then write the bytes from your buffer to your file by calling the WriteBufferAsync method of the FileIO class.
			await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer);

			//	Writing text to a file by using a stream(4 step2)
			//First, open the file by calling the StorageFile.OpenAsync method.It returns a stream of the file's content when the open operation completes.
			using (var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
			{
				//	Next, get an output stream by calling the GetOutputStreamAt method from the stream.Put this in a using statement to manage the output stream's lifetime.
				using (var outputStream = stream.GetOutputStreamAt(0))
				{
					//Now add this code within the existing using statement to write to the output stream by creating a new DataWriter object and calling the DataWriter.WriteString method.
					using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
					{
						dataWriter.WriteString("DataWriter has methods to write to various types, such as DataTimeOffset.");

						//Lastly, add this code (within the inner using statement) to save the text to your file with StoreAsync and close the stream with FlushAsync.
						await dataWriter.StoreAsync();
					}
					await outputStream.FlushAsync();
				}
				stream.Dispose(); // Or use the stream variable (see previous code snippet) with a using statement as well.
			}

			//Reading from a file
			//Here's how to read from a file on disk using the StorageFile class. The common first step for each of the ways of reading from a file is to get the file with StorageFolder.GetFileAsync.

			/*Windows.Storage.StorageFolder*/
			storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
			/*Windows.Storage.StorageFile */
			sampleFile = await storageFolder.GetFileAsync(filename);

			//Reading text from a file
			//Read text from your file by calling the ReadTextAsync method of the FileIO class.
			string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

			//Reading bytes from a file by using a buffer(2 step2)
			//First, read bytes from your buffer to your file by calling the ReadBufferAsync method of the FileIO class.
			/*var */
			buffer = await Windows.Storage.FileIO.ReadBufferAsync(sampleFile);

			//Then use a DataReader object to read first the length of the buffer and then its contents.
			using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
			{
				/*string */
				text = dataReader.ReadString(buffer.Length);
			}

			//Reading text from a file by using a stream(4 step2)

			//Open a stream for your file by calling the StorageFile.OpenAsync method.It returns a stream of the file's content when the operation completes.
			using (var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
			{
				ulong size = stream.Size;//Get the size of the stream to use later.

				using (var inputStream = stream.GetInputStreamAt(0)) //Get an input stream by calling the GetInputStreamAt method.Put this in a using statement to manage the stream's lifetime. Specify 0 when you call GetInputStreamAt to set the position to the beginning of the stream.
				{
					//Lastly, add this code within the existing using statement to get a DataReader object on the stream then read the text by calling DataReader.LoadAsync and DataReader.ReadString.
					using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
					{
						uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
						/*string */
						text = dataReader.ReadString(numBytesLoaded);
					}
				}
			}
		}
	}
}