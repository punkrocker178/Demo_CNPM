﻿using Demo.Models;
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

        public bool Update(ItemViewModel viewModel)
        {
            try
            {
                Item item = store.Item.Find(viewModel.Id);
                Category category = store.Category.Find(Int32.Parse(viewModel.Category));
                item.Name = viewModel.Name;
                item.Price = viewModel.Price;
                item.Description = viewModel.Description;
                item.Category = category;
                store.SaveChanges();
                return true;
            } catch (Exception e)
            {
                return false;
            }
           

        }

        public IEnumerable<ItemViewModel> GetViewListModel()
        {
            var list = new List<ItemViewModel>();
            var items = store.Item.Include(item => item.Category).Include(item => item.Image);
            foreach (Item item in items)
            {
                ItemViewModel model = new ItemViewModel(item.Id, item.Name, item.Price, item.Description, null);
                if (item.Category != null)
                {
                    model.Category = item.Category.Name;
                }
                if (item.Image != null)
                {
                    model.Images = item.Image;
                }
                list.Add(model);
                
            }
            return list;
        }
    }
}
