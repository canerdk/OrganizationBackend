using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using Business.Utilities.Security.JWT;
using DataAccess.Utilities.Auth;
using Entities.Common;
using Entities.Dtos.Auth;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Security.Claims;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IJwtManager _jwtManager;
        private readonly ILoginUserManager _loginUserManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IJwtManager jwtManager, ILoginUserManager loginUserManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtManager = jwtManager;
            _loginUserManager = loginUserManager;
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
                    TenantId = user.TenantId,
                    UserName = user.UserName,
                    Roles = userRoles.ToList(),
                    IsMain = user.IsMain
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
                TenantId = register.TenantId,
                EmailConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                IsMain = true
            };

            var result = await _userManager.CreateAsync(appUser, register.Password);
            if (result.Succeeded)
                return new SuccessResult();
            else
                return new ErrorResult(result.Errors.Select(x => x.Description));
        }

        public async Task<IResult> RegisterBranchUser(RegisterBranchUserDto register)
        {
            var appUser = new AppUser()
            {
                UserName = register.UserName,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                PhoneNumber = register.Phone,
                TenantId = _loginUserManager.GetLoginUserTenant(),
                EmailConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                IsMain = false,
                ParentId = _loginUserManager.GetLoginUserId()
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
                TenantId = user.TenantId,
                UserName = user.UserName,
                Roles = userRoles.ToList(),
                IsMain = user.IsMain
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

        public async Task<IResult> AssignReportClaims(UserReportClaimDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var existClaim = claims.FirstOrDefault(x => x.Type == dto.ReportType.ToString());
                if (existClaim != null)
                {
                    var ids = string.Join(",", dto.ReportIds);
                    var newClaim = new Claim(dto.ReportType.ToString(), ids);
                    var result = await _userManager.ReplaceClaimAsync(user, existClaim, newClaim);
                    if (result.Succeeded)
                        return new SuccessResult("Claims updated");
                    else
                        return new ErrorResult(result.Errors);
                }
                else
                {
                    var ids = string.Join(",", dto.ReportIds);
                    var claim = new Claim(dto.ReportType.ToString(), ids);
                    var result = await _userManager.AddClaimAsync(user, claim);
                    if (result.Succeeded)
                        return new SuccessResult("Claims added");
                    else
                        return new ErrorResult(result.Errors);
                }
            }

            return new ErrorResult("User not found");
        }

        public async Task<IDataResult<List<int>>> GetUserReportClaims(int userId, ReportType reportType)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var result = await _userManager.GetClaimsAsync(user);
                if (result != null)
                {
                    var claim = result?.FirstOrDefault(x => x.Type == reportType.ToString())?.Value;
                    if (!string.IsNullOrEmpty(claim))
                    {
                        var reportIds = claim.Split(",").Select(id => int.Parse(id)).ToList();
                        return new SuccessDataResult<List<int>>(reportIds);
                    }
                }
            }

            return new ErrorDataResult<List<int>>();
        }

        public async Task<IDataResult<IEnumerable<UserReportClaimDto>>> GetAllUserReportClaims(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var result = await _userManager.GetClaimsAsync(user);
                if (result != null)
                {
                    List<UserReportClaimDto> claims = new List<UserReportClaimDto>();
                    foreach (var item in result)
                    {
                        if (Enum.TryParse(item.Type, out ReportType reportType))
                        {
                            claims.Add(new UserReportClaimDto()
                            {
                                UserId = userId,
                                ReportType = reportType,
                                ReportIds = item.Value.Split(",").Select(id => int.Parse(id)).ToList()
                            });
                        }
                    }
                    return new SuccessDataResult<IEnumerable<UserReportClaimDto>>(claims);
                }
            }

            return new ErrorDataResult<IEnumerable<UserReportClaimDto>>();
        }

        public async Task<IDataResult<IEnumerable<ClaimDto>>> GetAllClaim()
        {
            var users = await _userManager.Users.Where(x => x.TenantId == _loginUserManager.GetLoginUserTenant()).ToListAsync();
            if (users != null)
            {
                List<ClaimDto> userClaims = new List<ClaimDto>();
                foreach (var user in users)
                {
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (claims != null)
                    {
                        foreach (var claim in claims)
                        {
                            userClaims.Add(new ClaimDto()
                            {
                                UserId = user.Id,
                                Type = claim.Type,
                                Value = claim.Value,
                                Email = user.Email,
                                UserName = user.UserName
                            });
                        }
                    }
                }
                return new SuccessDataResult<IEnumerable<ClaimDto>>(userClaims);

            }

            return new ErrorDataResult<IEnumerable<ClaimDto>>();
        }

        public async Task<IDataResult<IEnumerable<UserDto>>> GetAllUser()
        {
            var users = await _userManager.Users.Where(x => x.TenantId == _loginUserManager.GetLoginUserTenant()).ToListAsync();

            if (users != null)
                return new SuccessDataResult<IEnumerable<UserDto>>(_mapper.Map<List<UserDto>>(users));

            return new ErrorDataResult<IEnumerable<UserDto>>();
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
