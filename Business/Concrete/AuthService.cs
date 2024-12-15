using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using Business.Utilities.Security.JWT;
using DataAccess.Utilities.Auth;
using Entities.Dtos.Auth;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IJwtManager _jwtManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IJwtManager jwtManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtManager = jwtManager;
            _mapper = mapper;
        }

        public async Task<IDataResult<TokenDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var userDto = new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Roles = userRoles.ToList()
                };

                string accessToken = _jwtManager.GenerateJwtToken(userDto);
                string refreshToken = _jwtManager.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await _userManager.UpdateAsync(user);

                return new SuccessDataResult<TokenDto>(new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }

            return new ErrorDataResult<TokenDto>("User not found!");
        }

        public async Task<IResult> Register(RegisterDto register)
        {
            var appUser = new AppUser()
            {
                UserName = register.UserName,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PhoneNumber = register.Phone,
                EmailConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                Type = register.Type
            };

            var result = await _userManager.CreateAsync(appUser, register.Password);
            if (result.Succeeded)
                return new SuccessResult();
            else
                return new ErrorResult(result.Errors.Select(x => x.Description));
        }

        public async Task<IDataResult<TokenDto>> Refresh(TokenDto token)
        {
            string accessToken = token.AccessToken;
            string refreshToken = token.RefreshToken;
            var principal = _jwtManager.GetPrincipalFromExpiredToken(accessToken);

            var user = await _userManager.FindByIdAsync(principal.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return new ErrorDataResult<TokenDto>("Invalid token");


            var userRoles = await _userManager.GetRolesAsync(user);

            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = userRoles.ToList()
            };

            var newAccessToken = _jwtManager.GenerateJwtToken(userDto);
            var newRefreshToken = _jwtManager.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new SuccessDataResult<TokenDto>(new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        public async Task<IResult> AddRoles(List<string> roles)
        {
            if (roles != null && roles.Any())
            {
                int count = 0;
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        var result = await _roleManager.CreateAsync(new AppRole()
                        {
                            Name = role
                        });
                        if (result.Succeeded)
                            count++;
                    }
                }
                return new SuccessResult($"Added {count} role");
            }

            return new ErrorResult();
        }

        public async Task<IResult> AssignRoles(int userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null && roles != null && roles.Any())
            {
                int count = 0;
                foreach (var role in roles)
                {
                    if (await _roleManager.RoleExistsAsync(role))
                    {
                        var result = await _userManager.AddToRoleAsync(user, role);
                        if (result.Succeeded)
                            count++;
                    }
                }

                return new SuccessResult($"Assigned {count} role to {user.UserName}");
            }

            return new ErrorResult("User not found");
        }

        public async Task<IResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return new SuccessResult("User deleted");
                else
                    return new ErrorResult(result.Errors);
            }

            return new ErrorResult("User not found");
        }

        public async Task<IResult> ChangePassword(ChangePasswordDto dto)
        {
            if (!dto.NewPassword.Equals(dto.NewPasswordRepeat))
                return new ErrorResult("Passwords mis match!");

            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

                if (result.Succeeded)
                    return new SuccessResult("Password changed");
                else
                    return new ErrorResult(result.Errors);
            }

            return new ErrorResult("User not found");
        }

        public async Task<IResult> UpdateUser(UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user != null)
            {
                var updateUser = _mapper.Map(dto, user);

                var result = await _userManager.UpdateAsync(updateUser);
                if (result.Succeeded)
                    return new SuccessResult();
                else
                    return new ErrorResult(result.Errors.Select(x => x.Description));
            }

            return new ErrorResult("User not found!");
        }

        public async Task<IDataResult<UserDto>> GetUserById(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
                return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(user));
            else
                return new ErrorDataResult<UserDto>("User not found");
        }
    }
}
