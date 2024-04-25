using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyWebRazor.Data;
using StudyWebRazor.Models;

namespace StudyWebRazor.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }
		public DeleteModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? CategoryId)
		{
			if (CategoryId != null && CategoryId != 0)
			{
				Category = _db.Categories.Find(CategoryId);
			}
		}

		public IActionResult OnPost()
		{
			Category obj = _db.Categories.Find(Category.CategoryId);
			if (obj == null)
			{
				return NotFound();
			}

			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted Successfully";
			return RedirectToPage("Index");
		}
	}

}
