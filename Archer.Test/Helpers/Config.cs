using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.Helpers
{
    public sealed class Config
    {
        private static readonly Config _instance = new Config();

        public static Config Instance
        {
            get
            {
                return _instance;
            }
        }

        public string DBName { get { return GetStringValue("DBName", "Demo_Data"); } }
        public string DropLocation { get { return GetStringValue("DropLocation", "_tmp"); } }
        public int TestRecordsCount { get { return GetIntValue("AmountofTestData", 100); } }
        public bool ClearTablesBeforeRun { get { return GetBoolValue("ClearTableBeforeRun", false); } }
        public bool DeleteFileAfterImport { get { return GetBoolValue("DeleteFileAfterImport", false); } }
        

        private bool GetBoolValue(string key, bool valueifEmpty)
        {
            string value = GetStringValue(key, valueifEmpty);
            bool outValue;
            if (!bool.TryParse(value, out outValue))
            {
                throw new FormatException("Unable to parse int value");
            }

            return outValue;
        }

        private int GetIntValue(string key, int valueifEmpty)
        {
            string value = GetStringValue(key, valueifEmpty);
            int outValue;
            if (!int.TryParse(value, out outValue))
            {
                throw new FormatException("Unable to parse int value");
            }
            
            return outValue;
        }

        private string GetStringValue(string key, object valueifEmpty)
        {
            try
            {
                string value = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
                else
                {
                    return (valueifEmpty ?? "").ToString();
                }
            }
            catch (KeyNotFoundException nfEx)
            {
                if (valueifEmpty != null)
                {
                    return valueifEmpty.ToString();
                }
                else
                {
                    throw nfEx;
                }
            }

        }
    }
}
