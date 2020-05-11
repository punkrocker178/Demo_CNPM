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

        public Image(string title, string path)
        {
            Title = title;
            Path = path;
        }
    }
}
