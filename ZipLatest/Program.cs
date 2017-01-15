using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace ZipLatest
{
    class Program
    {
        static string _zipFilePath = string.Empty;
        static bool _useDateFormat = false;
        static string _dateFormat = string.Empty;
        static int _compressionLevel = 0;
        static bool _useLogging = false;

        static int _totalDirs = 0;
        static int _totalFiles = 0;

        static void Main(string[] args)
        {
            zipIt(args);

            //Console.ReadLine();
        }

        static void zipIt(string[] args)
        {
            if (!args.Any())
            {
                return;
            }

            string tempFile = args[0];
            DateTime now = DateTime.Now;

            _dateFormat = helpers.GenericHelper.GetSetting("DateFormat", "yyyy-MM-dd");
            // REMOVE SPECIAL CHARACTERS FROM PART OF FILENAME, WHERE NOT ALLOWED 
            _dateFormat = helpers.GenericHelper.CleanDateFormat(_dateFormat); 

            // GET COMPRESSION LEVEL. RANGE 0 TO 9 ACCEPTED
            _compressionLevel = helpers.GenericHelper.GetSettingAsInt("CompressionLevel", 3);
            _compressionLevel = helpers.GenericHelper.ValidateCompression(_compressionLevel, 3, 0, 9);
            _useLogging = helpers.GenericHelper.GetSettingAsBool("UseLogging", false);
            helpers.Logger.UseLogging = _useLogging;

            // GET FIRST FILENAME WITH DATETIME STAMP TO NAME ZIP FILE
            string firstFileName = string.Empty;
            string suffix = string.Empty;

            // ADD TO SUFFIX, IF DATE/TIME IS REQUIRED
            if (_useDateFormat)
            {
                suffix = string.Format(" {0}", now.ToString(_dateFormat));
            }

            // CREATE FILENAME
            firstFileName = string.Format("{0}",
                System.IO.Path.GetFileNameWithoutExtension(tempFile),
                suffix).Trim();

            // SET DIRECTORY PATH TO SAVE ZIP FILE
            string topDirectory = System.IO.Path.GetDirectoryName(tempFile);

            // CREATE ZIP FILENAME WITH DATE AND VERSION NUMBER. 
            _zipFilePath = helpers.GenericHelper.ValidateZipFileName(topDirectory, firstFileName);

           helpers.Logger.WriteEntry(string.Format("New Zip file: {0}", _zipFilePath), true);

            using (FileStream fsOut = File.Create(_zipFilePath))
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(fsOut))
                {
                    zipStream.SetLevel(_compressionLevel);
                    foreach (string s in args)
                    {
                        zipAsset(s, zipStream);
                    }
                }
            }

            // LOG TOTAL OF ZIPPED FILES AND DIRECOTORIES.
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Zipped: {0} directories and {1} files", _totalDirs, _totalFiles);

            helpers.Logger.WriteEntry(sb.ToString());
        }

        static void zipAsset(string asset, ZipOutputStream zipStream)
        {
            if (System.IO.Directory.Exists(asset))
            {
                string[] files = System.IO.Directory.GetFiles(asset);

                foreach (string f in files)
                {
                    zipAsset(f, zipStream);
                }

                string[] dirs = System.IO.Directory.GetDirectories(asset);

                if (dirs.Any())
                {
                    foreach (string d in dirs)
                    {
                        zipAsset(d, zipStream);
                        _totalDirs += 1;
                    }
                }

            } else
            {
                if (!System.IO.File.Exists(asset))
                {
                    return;
                }

                Console.WriteLine("Zipping file: {0}", asset);
                FileInfo fi = new FileInfo(asset);

                string entryName = ZipEntry.CleanName(fi.FullName);

                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime;
                
                //newEntry.Size = fi.Length;
                zipStream.UseZip64 = UseZip64.Off;
                zipStream.PutNextEntry(newEntry);

                byte[] buffer = new byte[4096];

                try
                {
                    using (FileStream streamReader = File.OpenRead(asset))
                    {
                        StreamUtils.Copy(streamReader, zipStream, buffer);
                    }
                    _totalFiles += 1;
                } catch (Exception ex)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendFormat("Failed to zip {0}. ", asset);
                    sb.AppendFormat("Exception: {0}", ex.Message);

                    helpers.Logger.WriteEntry(sb.ToString());
                }
                

                zipStream.CloseEntry();
            }

            
        }

    }
}
