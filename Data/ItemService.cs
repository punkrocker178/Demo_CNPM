using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data
{
    public class ItemService
    {
        private StoreContext store;
       
        public ItemService(StoreContext context)
        {
            this.store = context;
        }

        public IEnumerable<Item> GetAll()
        {
            return store.Item;
        }

        public bool AddItem(Item item)
        {
            try
            {
                store.Item.Add(item);
                store.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // assume that the 'item' object is previously retrived from database via LaptopContext
        public bool UpdateItem(Item item)
        {
            try
            {
                //store.Entry(item).State = System.Data.Entity.EntityState.Modified;
                store.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteItem(Item item)
        {
            try
            {
                store.Item.Remove(item);
                store.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // get all laptops by cpu name
        //public List<Laptop> GetLaptopByCPU(String cpu)
        //{
        //    return db.Laptops.Where(laptop => laptop.CPU == cpu).ToList<Laptop>();
        //}

        /*
        public IEnumerable<Laptop> GetLaptopHasRamEqualOrGreaterThan(int ram)
        {
            var data = from laptop in db.Laptops
                       where laptop.RAM >= ram
                       select laptop;

            return data;
        }*/

        public int GetItemCount()
        {
            return (from item in store.Item
                    select item).Count();
        }

        public  Item GetItem(int id)
        {
            return store.Item
                .Include(item => item.Category)
                .Include(item => item.Image)
                .FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<ItemViewModel> GetViewListModel()
        {
            var list = new List<ItemViewModel>();
            var items = store.Item.Include(items => items.Category);
            foreach (Item item in items)
            {
                if (item.Category != null)
                {
                    list.Add(new ItemViewModel(item.Id, item.Name, item.Price, item.Description, item.Category.Name));
                } else
                {
                    list.Add(new ItemViewModel(item.Id, item.Name, item.Price, item.Description, null));
                }
             
                
            }
            return list;
        }
    }
}
