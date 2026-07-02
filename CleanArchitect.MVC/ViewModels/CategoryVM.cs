using CleanArchitect.Application.DTOs.BikeShop;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitect.MVC.ViewModels
{
    [Bind("Id, Name, Description")]
    public class CategoryVM
    {
        public CategoryVM()
        {

        }

        public CategoryVM(CategoryDTO dtoCategory)
        {
            Id = dtoCategory.Id;
            Name = dtoCategory.Name;
            Description = dtoCategory.Description;
            Position = dtoCategory.Position;
        }

        public static CategoryDTO ConvertToCategoryDTO(CategoryVM categoryVM)
        {
            return new CategoryDTO
            {
                Id = categoryVM.Id,
                Name = categoryVM.Name,
                Description = categoryVM.Description,
                Position = categoryVM.Position
            };
        }

        public Guid Id { get; set; }

        [MaxLength(150, ErrorMessage = " Không được nhập quá 150 ký tự nha mấy cha nội")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string? Description { get; set; }

        public int Position { get; set; }



    }
}
