# ZipLatest
When you drop a file or folder into ZipLatest application, it will automatically create a zip filename based on the first file or directory name. 

# Dependancies
It uses SharpZipLib (Version 0.86.0). 

# App.config
There are settings in the App.config where you can do the following.

UseDateInFileName - Boolean value. Set true if you want to use a date/timestamp in zipfile name e.g. "Foo 2017-01-15.zip"
DateFormat - Set your own date format. Whatever datet/time format you use, it will be loaded into DateTime.ToString() method. Examples: yyyy-MM-dd will change to 2017-01-15, ddMMyyyy will change to 15012017. This won't change if you UseDateInFileName as false. See link for more info on DateTime.ToString() method https://msdn.microsoft.com/en-us/library/zdtaw1bw.aspx.
CompressionLevel - Integer-based. This sets the compression level of each file. This application uses SharpZipLib, available from nuget, their accepted range of compression level is between 0 and 9. Any values outside this range will default to 0.
UseLogging - Boolean value. Writes entries to a date stamped text file.  Can be found in the same directory as the ZipLatest application.
