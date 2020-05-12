using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }

        public string Url { get; set; }

        public Image(string title, string path, string url)
        {
            Title = title;
            Path = path;
            Url = url;
        }
    }
}
