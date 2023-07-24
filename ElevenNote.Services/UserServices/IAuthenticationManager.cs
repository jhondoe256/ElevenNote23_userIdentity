using ElevenNote.Models.UserModels;
using Microsoft.AspNetCore.Identity;

namespace ElevenNote.Services.UserServices
{
    public interface IAuthenticationManager
    {
        Task<IEnumerable<IdentityError>> Register(UserEntityVM userEntity);

        Task<AuthResponseVM> Login(LoginVM loginVM);
    }
}