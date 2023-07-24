using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ElevenNote.Data.Entities;
using ElevenNote.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ElevenNote.Services.UserServices
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        private string USERNAME = "";

        public AuthenticationManager(IConfiguration configuration, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AuthResponseVM> Login(LoginVM loginVM)
        {
            //find user by email
            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            bool isValidUser = await _userManager.CheckPasswordAsync(user!, loginVM.Password);

            if (user is null || isValidUser == false)
            {
                return new AuthResponseVM();
            }

            var token = await GenerateToken(user);

            return new AuthResponseVM
            {
                UserName = user.Email!,
                Token = token
            };
        }


        private async Task<string> GenerateToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim("name",$"{user.FullName}"),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email!), // the person whom this is being issued
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), //Use this to prevent Jwt Spoofing
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                //we can have our own claims
                new Claim("uId",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            USERNAME = claims.FirstOrDefault(x => x.Type == "name")!.Value;

            //construct the overall token
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(14),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<IEnumerable<IdentityError>> Register(UserEntityVM userEntity)
        {
            var user = _mapper.Map<UserEntity>(userEntity);

            user.UserName = userEntity.Email;
            user.DateCreated = DateTime.UtcNow;

            //create the password
            var result = await _userManager.CreateAsync(user, userEntity.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
    }
}