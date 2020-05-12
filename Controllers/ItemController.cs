using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    public class ItemController: Controller
    {
        // GET: /<controller>/

        private readonly StoreContext _context;
        private ItemService itemService;
        private CategoryService categoryService;

        public ItemController(StoreContext context)
        {
            _context = context;
            itemService = new ItemService(context);
            categoryService = new CategoryService(context);
        }

        public IActionResult Index()
        {
            return View(itemService.GetViewListModel());
        }

        public IActionResult Home()
        {
            return View("Home", itemService.GetViewListModel());
        }

        public IActionResult CreateView()
        {
            // Init category dropdown
            var categoryList = new List<SelectListItem>();
            foreach (Category category in categoryService.GetAll()) {
                categoryList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            ViewData["Categories"] = new SelectList(categoryList, "Value", "Text");
            return View("Create");
        }

        public IActionResult Create(ItemViewModel item)
        {
            Item model = new Item();
            model.Name = item.Name;
            model.Price = item.Price;
            model.Description = item.Description;
            model.Category = categoryService.Get(item.Category);
            itemService.AddItem(model);
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            Item item = itemService.GetItem(id);
            ViewData["isEdit"] = false;
            if (item != null)
            {
                ItemViewModel viewModel = new ItemViewModel(item.Id, item.Name, item.Price, item.Description, item.Category.Name);
                if (item.Image != null)
                {
                    viewModel.Images = item.Image;
                }
                return View("Detail", viewModel);
            }
            return View("Error", new { ErrorMessage = "Error fetching"});
        }

        public IActionResult EditView(int id)
        {
            // Init category dropdown
            var categoryList = new List<SelectListItem>();
            foreach (Category category in categoryService.GetAll())
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            ViewData["Categories"] = new SelectList(categoryList, "Value", "Text");

            Item item = itemService.GetItem(id);
            ViewData["isEdit"] = true;
            if (item != null)
            {
                ItemViewModel viewModel = new ItemViewModel(item.Id, item.Name, item.Price, item.Description, item.Category.Name);
                if (item.Image != null)
                {
                    viewModel.Images = item.Image;
                }
                return View("Detail", viewModel);
            }
            ViewData["ErrorMessage"] = "Fetching Failed";
            return View("Error");
        }

        public IActionResult Edit(ItemViewModel model)
        {
           if (itemService.Update(model))
            {
                ViewData["isEdit"] = false;
                return RedirectToAction("Detail", new { id = model.Id});
            }
            ViewData["ErrorMessage"] = "Update Failed";
            return View("Error");
        }

        public IActionResult Delete(int id)
        {
            Item item = itemService.GetItem(id);
            itemService.DeleteItem(item);
            return RedirectToAction("Index");
        }
    }
}
