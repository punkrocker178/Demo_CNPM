using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data
{
    public class ImageService
    {
        private StoreContext store;

        public ImageService(StoreContext context)
        {
            this.store = context;
        }

        public Image Get(int id)
        {
            Image image = store.Image.Find(id);
            if (image == null)
            {
               throw new Exception("Not found");
            }
            return image;
        }

        public Image GetByName(string name)
        {
            Image image = store.Image.Where(image => image.Title == name).FirstOrDefault();
            if (image == null)
            {
                throw new Exception("Not found");
            }
            return image;
        }

        public bool Add(Image image, Item item)
        {
            try
            {
                if (item != null)
                {
                    if (item.Image == null)
                    {
                        item.Image = new Collection<Image>();
                    }
                    item.Image.Add(image);
                    store.SaveChanges();
                    return true;
                }
                return false;
                
            } catch (Exception e)
            {
                return false;
            }
           
        }
    }
}
