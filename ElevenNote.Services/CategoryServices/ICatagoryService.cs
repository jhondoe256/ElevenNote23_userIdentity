using ElevenNote.Models.CategoryModels;

namespace ElevenNote.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CategoryCreateVM model);

        Task<bool> UpdateCategory(CategoryEditVM model);

        Task<bool> DeleteCategory(int id);

        Task<CategoryDetailVM> GetCategory(int id);

        Task<List<CategoryListItemVM>> GetCategories();
    }
}