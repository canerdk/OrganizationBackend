using Business.Utilities.Results;
using Entities.Dtos.Auth;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> Register(RegisterDto register);
        Task<IResult> UpdateUser(UpdateUserDto dto);
        Task<IDataResult<TokenDto>> Login(LoginDto login);
        Task<IDataResult<TokenDto>> Refresh(TokenDto token);
        Task<IResult> Delete(int userId);
        Task<IResult> AddRoles(List<string> roles);
        Task<IResult> AssignRoles(int userId, List<string> roles);
        Task<IDataResult<UserDto>> GetUserById(int userId);
        Task<IResult> ChangePassword(ChangePasswordDto dto);
    }
}
