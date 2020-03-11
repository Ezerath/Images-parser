using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseImages
{
    public class Image
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Image(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
