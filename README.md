# ZipLatest
When you drag and drop a file or folder into ZipLatest application (ziplatest.exe), it will automatically create a zip filename based on the first file or directory name. You can include a date/time stamp in the filename.

If the file already exists, it will add an incremented value to the filename. If the file "Foo 2017-01-15.zip" already exists, it will add an integer value as a suffix i.e. "Foo 2017-01-15 (1).zip". It will increment each time the filename exists.

# How To Use

Create a shortcut to the application and copy to the "Send To" directory. This will allow you to right click a file and select "Send To > ZipLatest.exe".

# Dependancies
It uses SharpZipLib (Version 0.86.0). 

# App.config
There are settings in the App.config where you can do the following.

UseDateInFileName - Boolean value. Set true if you want to use a date/timestamp in zipfile name e.g. "Foo 2017-01-15.zip"

DateFormat - Set your own date format. Whatever datet/time format you use, it will be loaded into DateTime.ToString() method. Examples: yyyy-MM-dd will change to 2017-01-15, ddMMyyyy will change to 15012017. This won't change if you UseDateInFileName as false. See link for more info on DateTime.ToString() method https://msdn.microsoft.com/en-us/library/zdtaw1bw.aspx.

CompressionLevel - Integer-based. This sets the compression level of each file. This application uses SharpZipLib, available from nuget, their accepted range of compression level is between 0 and 9. Any values outside this range will default to 0.

UseLogging - Boolean value. Writes entries to a date stamped text file.  Can be found in the same directory as the ZipLatest application.

# Refactored code
I refactored the code available from SharpZipL:ib. https://github.com/icsharpcode/SharpZipLib/wiki/Zip-Samples#anchorCreate

I made it to loop through each directory amd check if any file exists

I added logic check if a file doesn't exists, it will skipm that file. I've used zipStream.UseZip64 instead of setting the file length. It will also log the filepath if it has failed to zip the file. This can happen if the file is locked or it's in use.

# Why create ZipLatest
I often back-up my stuff on my computer. In Windows 10, I was getting tired of right-clicking a selection of files and folders, and then select "Send to > Compressed (zipped) folder" then I would have to rename the zip filename with a date/timestamp.

This saves me the time and effort.
