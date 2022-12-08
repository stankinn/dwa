using System;
using System.Collections.Generic;

namespace WpfApp2019
{
    public class lego
    {
        public string rcsrc { get; set; }
        public DateTime ldts { get; set; }

        public List<string> dataTypes = new List<string>();
        public List<string> dataNames = new List<string>();

        public lego(string rcsrc, DateTime ldts, List<string> dataTypes, List<string> dataNames)
        {
            this.rcsrc = rcsrc;
            this.ldts = ldts;
            this.dataTypes = dataTypes;
            this.dataNames = dataNames;
        }
    }
}