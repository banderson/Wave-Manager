using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveManagerBusiness
{
    public static class CompanyInfo
    {
        /// <summary>
        /// Public properties that describe the Company itself
        /// </summary>
        public static string Name
        {
            get
            {
                return "The Context Switch, Inc.";
            }
        }

        /// <summary>
        /// Public Properties that describe the product in use
        /// </summary>
        public static string ProductName
        {
            get
            {
                return "Wave Manager";
            }
        }

        public static string ProductAuthor
        {
            get
            {
                return "Ben Anderson";
            }
        }

        public static string CompanyURL
        {
            get
            {
                return "http://thecontextswitch.com";
            }
        }

        public static string ProductDescription
        {
            get
            {
                return "This program allows you manage you collection of WAV files, including: playback, modulation, and managing your files.";
            }
        }

        public static string ProductVersion
        {
            get
            {
                return "1.0.0";
            }
        }
    }
}
