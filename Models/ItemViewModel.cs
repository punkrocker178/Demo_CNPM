using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public IEnumerable<Image> Images { get; set; }

        public ItemViewModel(int Id, string Name, int Price, string Desciption, string Category)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Description = Desciption;
            this.Category = Category;
        }

        public ItemViewModel()
        {

        }
    }
}
