using Business.Utilities.Results;
using Entities.Dtos.Auth;
using Entities.Enums;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> Register(RegisterDto register);
        Task<IResult> RegisterBranchUser(RegisterBranchUserDto register);
        Task<IResult> UpdateUser(UpdateUserDto dto);
        Task<IDataResult<TokenDto>> Login(LoginDto login);
        Task<IDataResult<TokenDto>> Refresh(TokenDto token);
        Task<IResult> Delete(int userId);
        Task<IResult> AddRoles(List<string> roles);
        Task<IResult> AssignRoles(int userId, List<string> roles);
        Task<IResult> AssignReportClaims(UserReportClaimDto claim);
        Task<IDataResult<List<int>>> GetUserReportClaims(int userId, ReportType reportType);
        Task<IDataResult<IEnumerable<UserReportClaimDto>>> GetAllUserReportClaims(int userId);
        Task<IDataResult<IEnumerable<UserDto>>> GetAllUser();
        Task<IDataResult<UserDto>> GetUserById(int userId);
        Task<IDataResult<IEnumerable<ClaimDto>>> GetAllClaim();
        Task<IResult> ChangePassword(ChangePasswordDto dto);
    }
}
