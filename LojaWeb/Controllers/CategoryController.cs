﻿using LojaWeb.Data;
using LojaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList = _db.Categories.ToList();
			return View(objCategoryList);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The Display Order cannot exactle match the Name.");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Category created Successfully";
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}
		public IActionResult Edit(int? categoryId)
		{
			if (categoryId == null || categoryId == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.Find(categoryId);
			//Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.CategoryId==categoryId);
			//Category? categoryFromDb2 = _db.Categories.Where(u=>u.CategoryId==categoryId).FirstOrDefault();

			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category updated Successfully";
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}

		public IActionResult Delete(int? categoryId)
		{
			if (categoryId == null || categoryId == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.Find(categoryId);
			//Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.CategoryId==categoryId);
			//Category? categoryFromDb2 = _db.Categories.Where(u=>u.CategoryId==categoryId).FirstOrDefault();

			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? categoryId)
		{
			Category obj = _db.Categories.Find(categoryId);
			if (obj == null)
			{
				return NotFound();
			}

			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted Successfully";
			return RedirectToAction("Index");
		}
	}
}
