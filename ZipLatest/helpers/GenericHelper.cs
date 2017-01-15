using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipLatest.helpers
{
    public class GenericHelper
    {

        public static string GetSetting(string name, string defaultVal)
        {
            string results = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                return defaultVal;
            }

            results = System.Configuration.ConfigurationManager.AppSettings[name];

            if (string.IsNullOrEmpty(results))
            {
                results = defaultVal;
            }

            return results;
        }

        public static int GetSettingAsInt(string name, int defaultVal)
        {
            int results = 0;

            if (string.IsNullOrEmpty(name))
            {
                return defaultVal;
            }

            string temp = System.Configuration.ConfigurationManager.AppSettings[name];

            if (string.IsNullOrEmpty(temp))
            {
                results = defaultVal;
            } else
            {
                bool isValid = int.TryParse(temp, out results);

                if (!isValid)
                {
                    results = defaultVal;
                }
            }

            return results;
        }

        public static bool GetSettingAsBool(string name, bool defaultVal)
        {
            bool results = false;

            if (string.IsNullOrEmpty(name))
            {
                return defaultVal;
            }

            string temp = System.Configuration.ConfigurationManager.AppSettings[name];

            if (string.IsNullOrEmpty(temp))
            {
                results = defaultVal;
            }
            else
            {
                bool isValid = bool.TryParse(temp, out results);

                if (!isValid)
                {
                    results = defaultVal;
                }
            }

            return results;
        }

        public static string ValidateZipFileName(string topDir, string zipFilename)
        {
            string results = string.Empty;

            string searchPattern = string.Format("*{0}*", zipFilename);

            string[] files = System.IO.Directory.GetFiles(topDir, searchPattern);

            if (files.Any())
            {
                int count = files.Length;
                zipFilename = string.Format("{0} ({1}).zip", zipFilename, count);
            }
            else
            {
                zipFilename = string.Format("{0}.zip", zipFilename);
            }

            results = System.IO.Path.Combine(topDir, zipFilename);

            return results;
        }

        public static int ValidateCompression(int val, int defaultVal, int minVal, int maxVal)
        {
            int results = val;

            if (!(val >= minVal && val <= maxVal))
            {
                results = defaultVal;
            }

            return results;
        }

        public static string CleanDateFormat(string val)
        {
            string results = val;

            // REMOVE SPECIAL CHARACTERS FROM POTENTIAL FILENAME
            results = results.Replace(":", "");

            return results;
        }

    }
}
