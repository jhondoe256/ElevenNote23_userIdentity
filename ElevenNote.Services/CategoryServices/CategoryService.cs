using AutoMapper;
using ElevenNote.Data.ElevenNoteContext;
using ElevenNote.Data.Entities;
using ElevenNote.Models.CategoryModels;
using Microsoft.EntityFrameworkCore;

namespace ElevenNote.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ElevenNoteDBContext _context;
        private readonly IMapper _mapper;
        public CategoryService(ElevenNoteDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateCategory(CategoryCreateVM model)
        {
            var entity = _mapper.Map<CategoryEntity>(model);

            await _context.Categories.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) return false;

            _context.Categories.Remove(category);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CategoryListItemVM>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoriesListItems = _mapper.Map<List<CategoryListItemVM>>(categories);

            return categoriesListItems;
        }

        public async Task<CategoryDetailVM> GetCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.Notes).SingleOrDefaultAsync(x => x.Id == id);

            if (category is null) return new CategoryDetailVM { };

            return _mapper.Map<CategoryDetailVM>(category);
        }

        public async Task<bool> UpdateCategory(CategoryEditVM model)
        {
            var category = await _context.Categories.Include(c => c.Notes).FirstOrDefaultAsync(x => x.Id == model.Id);

            if (category is null) return false;

            category.Name = model.Name;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}