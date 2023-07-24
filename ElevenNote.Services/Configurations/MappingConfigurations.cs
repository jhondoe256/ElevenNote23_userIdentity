using AutoMapper;
using ElevenNote.Data.Entities;
using ElevenNote.Models.CategoryModels;
using ElevenNote.Models.NoteModels;
using ElevenNote.Models.UserModels;

namespace ElevenNote.Services.Configurations
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations()
        {
            CreateMap<CategoryEntity,CategoryCreateVM>().ReverseMap();
            CreateMap<CategoryEntity,CategoryListItemVM>().ReverseMap();
            CreateMap<CategoryEntity,CategoryDetailVM>().ReverseMap();
            CreateMap<CategoryEntity,CategoryEditVM>().ReverseMap();

            CreateMap<NoteEntity,NoteCreateVM>().ReverseMap();
            CreateMap<NoteEntity,NoteListItemVM>().ReverseMap();
            CreateMap<NoteEntity,NoteDetailVM>().ReverseMap();
            CreateMap<NoteEntity,NoteEditVM>().ReverseMap();

            CreateMap<UserEntity,UserEntityVM>().ReverseMap();
        }
    }
}