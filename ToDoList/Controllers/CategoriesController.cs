using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {
    // [HttpGet("/categories")]
    // public ActionResult Index()
    // {
    //     List<Category> allCategories = Category.GetAll();
    //     return View(allCategories);
    // }
    // [HttpPost("/categories")]
    // public ActionResult Create(string categoryName)
    // {
    //   Category newCategory = new Category(categoryName);
    //   newCategory.Save();
    //   List<Category> allCategories = Category.GetAll();
    //   return View("Index", allCategories);
    // }
    // [HttpGet("/categories/new")]
    // public ActionResult New()
    // {
    //     return View();
    // }
    // This one creates new Items within a given Category, not new Categories:
    // [HttpPost("/categories/{categoryId}/items")]
    // public ActionResult Create(int categoryId, string itemDescription)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category foundCategory = Category.Find(categoryId);
    //   Item newItem = new Item(itemDescription, categoryId);
    //   newItem.Save();
    //   //foundCategory.AddItem(newItem);
    //   List<Item> categoryItems = foundCategory.GetItems();
    //   model.Add("items", categoryItems);
    //   model.Add("category", foundCategory);
    //   return View("Show", model);
    // }
    // [HttpGet("/categories/{id}")]
    // public ActionResult Show(int id)
    // {
    //     Dictionary<string, object> model = new Dictionary<string, object>();
    //     Category selectedCategory = Category.Find(id);
    //     List<Item> categoryItems = selectedCategory.GetItems();
    //     //Item item = Item.Find(itemId);
    //
    //     model.Add("category", selectedCategory);
    //     model.Add("items", categoryItems);
    //     return View(model);
    // }
    // [HttpPost("/categories/delete")]
    // public ActionResult Delete()
    // {
    //     Category.ClearAll();
    //     Item.ClearAll();
    //     return View();
    // }
    //
    // [HttpGet("/categories/{categoryId}/edit")]
    //   public ActionResult Edit(int categoryId)
    //   {
    //     Category category = Category.Find(categoryId);
    //     return View(category);
    //   }
    // [HttpPost("/categories/{categoryId}/update")]
    //   public ActionResult Update(int categoryId, string newName)
    //   {
    //     Category category = Category.Find(categoryId);
    //     category.Edit(newName);
    //     List<Category> allCategories = Category.GetAll();
    //     return View("Index", allCategories);
    //   }

    // [ActionName("Destroy"), HttpPost("/categories/{id}/delete")]
    // public ActionResult Destroy(int id)
    // {
    //   Category category = Category.Find(id);
    //   List<Item> categoryItems = category.GetItems();
    //   foreach(Item item in categoryItems)
    //   {
    //     item.Delete();
    //   }
    //   category.Delete();
    //   return RedirectToAction("Index");
    // }
  }
}
