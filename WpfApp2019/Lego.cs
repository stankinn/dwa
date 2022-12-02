using System;

namespace WpfApp2019
{
    public class lego_set
    {
        public string rcsrc { get; set; }
        public DateTime ldts { get; set; }
        public string set_num { get; set; }
        public string Name { get; set; }
        public int year { get; set; }
        public int theme_id { get; set; }
        public int num_parts { get; set; }

        public lego_set(string rcsrc, DateTime ldts, string set_num, string name, int year, int theme_id, int num_parts)
        {
            this.rcsrc = rcsrc;
            ldts = ldts;
            this.set_num = set_num;
            Name = name;
            this.year = year;
            this.theme_id = theme_id;
            this.num_parts = num_parts;
        }
    }

    public class lego_theme
    {
        public string rcsrc { get; set; }
        public DateTime ldts { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public int parent_id { get; set; }

        public lego_theme(string rcsrc, DateTime ldts, int id, string name, int parent_id)
        {
            this.rcsrc = rcsrc;
            ldts = ldts;
            this.id = id;
            Name = name;
            this.parent_id = parent_id;
        }
    }
}