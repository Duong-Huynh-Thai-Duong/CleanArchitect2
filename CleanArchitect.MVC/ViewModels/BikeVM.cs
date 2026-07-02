using CleanArchitect.Application.DTOs.BikeShop;
using CleanArchitect.Common.Configs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitect.MVC.ViewModels
{
    [Bind("Id,Name,Description,Image,Year,BrandName,CategoryId")]
    public class BikeVM
    {
        public BikeVM()
        {
            
        }

        public BikeVM(BikeDTO dtoBike)
        {
            Id = dtoBike.Id;
            Name = dtoBike.Name;
            BrandName = dtoBike.BrandName;
            CategoryId = dtoBike.CategoryId;
            Description = dtoBike.Description;
            Position = dtoBike.Position;
            Year = dtoBike.Year;
            ImagePath = !string.IsNullOrEmpty(dtoBike.ImagePath)
                ? Path.Combine("~/", AppConstants.ImageFolderPath, dtoBike.ImagePath)
                : Path.Combine("~/", AppConstants.ImageDefault);
        }

        public static BikeDTO ToDTO(BikeVM bikeVM)
        {
            return new BikeDTO
            {
                Id = bikeVM.Id,
                Name = bikeVM.Name,
                BrandName = bikeVM.BrandName,
                CategoryId = bikeVM.CategoryId,
                Description = bikeVM.Description,
                Position = bikeVM.Position,
                Year = bikeVM.Year,
                ImagePath = bikeVM.ImagePath
            };
        }

        public Guid Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string? Description { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }

        public int Position { get; set; }

        [Range(1200, 9999)]
        public int Year { get; set; }

        [MaxLength(150)]
        public string BrandName { get; set; } = string.Empty;
        //public string? Image { get; set; }

        //[MaxLength(150)]
        public Guid? CategoryId { get; set; }
        public string? Category { get; set; }
    }
}
