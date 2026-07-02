using CleanArchitect.Application.DTOs.BikeShop;
using CleanArchitect.Application.Interfaces;
using CleanArchitect.Domain.Entities.BikeShop;
using CleanArchitect.Infrastructure.DbContexts;
using CleanArchitect.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitect.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly EBikeShopDbContext _context;
        private readonly ICategoryService _serviceCategory;

        public CategoriesController(ICategoryService serviceCategory)
        {
            //_context = context;
            _serviceCategory = serviceCategory;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var cates = await _serviceCategory.GetAll();
            return View(cates);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? idCategory)
        {
            if (idCategory == null)
            {
                return NotFound();
            }

            var dtoCategory = await _serviceCategory.GetById(idCategory.Value);
            if (dtoCategory == null)
            {
                return NotFound();
            }
            var categoryVM = new CategoryVM(dtoCategory);

            return PartialView(categoryVM);
        }

        // GET: Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                var dtoCategory = new CategoryDTO
                {
                    Name = categoryVM.Name.Trim(),
                    Description = categoryVM.Description?.Trim(),
                };
                var categgory = await _serviceCategory.Create(dtoCategory);
                if (categgory != null)
                {
                    return RedirectToAction(nameof(Create));
                }
            }
            return View(categoryVM);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _context.Categories.FindAsync(id);
            var dtoCategory = await _serviceCategory.GetById(id.Value);
            if (dtoCategory == null)
            {
                return NotFound();
            }
            //var categoryVM = new CategoryVM
            //{
            //    Id = category.Id,
            //    Name = category.Name,
            //    Description = category.Description,
            //    Position = category.Position
            //};
            var categoryVM = new CategoryVM(dtoCategory);

            return View(nameof(Create), categoryVM);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryVM categoryVM)
        {
            if (id != categoryVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(category);
                    //var oldCate = await _context.Categories.FindAsync(categoryVM.Id);
                    //if (oldCate != null)
                    //{
                    //    oldCate.Name = categoryVM.Name.Trim();
                    //    oldCate.Description = categoryVM.Description?.Trim();
                    //    await _context.SaveChangesAsync();
                    //}
                    //var dtoCategory = new CategoryDTO
                    //{
                    //    Id = categoryVM.Id,
                    //    Name = categoryVM.Name.Trim(),
                    //    Description = categoryVM.Description?.Trim(),
                    //    Position = categoryVM.Position
                    //};

                    var dtoCategory = CategoryVM.ConvertToCategoryDTO(categoryVM);
                    var result = await _serviceCategory.Update(dtoCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoryExists(categoryVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Create));
            }
            return View(nameof(Create), categoryVM);
            //return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? idCategory)
        {
            if (idCategory == null)
            {
                return NotFound();
            }

            //var categoryVM = await _context.Categories
            //    .Select(c => new CategoryVM
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        Description = c.Description,
            //        Position = c.Position
            //    })
            //    .SingleOrDefaultAsync(m => m.Id == idCategory);
            var dtoCategory = await _serviceCategory.GetById(idCategory.Value);

            if (dtoCategory == null)
            {
                return NotFound();
            }

            var categoryVM = new CategoryVM(dtoCategory);

            return PartialView(categoryVM);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid idCategory)
        {
            var isOK = false;
            //var category = await _context.Categories.FindAsync(idCategory);
            //if (category != null)
            //{
            //    _context.Categories.Remove(category);
            //}

            //await _context.SaveChangesAsync();
            isOK = await _serviceCategory.Delete(idCategory);
            
            return Json(new { isOK});
        }

        public IActionResult CategoryList()
        {
            return ViewComponent(nameof(CategoryList));
        }

        private async Task<bool> CategoryExists(Guid id)
        {
            var category = await _serviceCategory.GetById(id);
            return category != null;
        }
    }

}
