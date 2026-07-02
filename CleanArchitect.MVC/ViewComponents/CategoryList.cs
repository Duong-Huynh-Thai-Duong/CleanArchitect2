using CleanArchitect.Application.Interfaces;
using CleanArchitect.Infrastructure.DbContexts;
using CleanArchitect.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitect.MVC.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        //private readonly EBikeShopDbContext _context;
        private readonly ICategoryService _categoryService;
        public CategoryList(ICategoryService categoryService)
        {
            //_context = dbContext;
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var categoryList = await _context.Categories.ToListAsync();
            //var categoryVMList = new List<CategoryVM>();
            //if (categoryList != null)
            //{
            //    foreach (var item in categoryList)
            //    {
            //        var cateVM = new CategoryVM
            //        {
            //            Id = item.Id,
            //            Name = item.Name,
            //            Description = item.Description,
            //            Position = item.Position,
            //        };
            //        categoryVMList.Add(cateVM);
            //    }
            //}
            //var categoryVMList = await _context.Categories
            //    .OrderByDescending(c => c.Position)
            //    .Select(c => new CategoryVM
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        Description = c.Description,
            //        Position = c.Position,
            //    })
            //    .ToListAsync();

            var categoryVMList = await _categoryService.GetAll();
            return View(categoryVMList);
        }
    }

}
