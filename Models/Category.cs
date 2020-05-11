using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
