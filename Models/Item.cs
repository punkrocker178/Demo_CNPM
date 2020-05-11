using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public int Price { get; set; }
        [AllowNull]
        public string Description { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Image { get; set; }
    }
}
