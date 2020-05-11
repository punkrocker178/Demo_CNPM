using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data
{
    public class CategoryService
    {
        private StoreContext store;
        public CategoryService(StoreContext context)
        {
            this.store = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return store.Category;
        }

        public Category Get(string id)
        {
            return store.Category.Find(Int32.Parse(id));
        }
    }
}
