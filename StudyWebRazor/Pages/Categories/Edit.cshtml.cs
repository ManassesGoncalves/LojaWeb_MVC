using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyWebRazor.Data;
using StudyWebRazor.Models;

namespace StudyWebRazor.Pages.Categories
{
	[BindProperties]
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }
		public EditModel(ApplicationDbContext db)
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
			if (ModelState.IsValid)
			{
				_db.Categories.Update(Category);
				_db.SaveChanges();
				TempData["success"] = "Category updated Successfully";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}