using Business.Utilities.Results;
using Entities.Dtos.UserContract;

namespace Business.Abstract
{
    public interface IUserContractService
    {
        Task<IDataResult<UserContractDto>> GetById(int id);
        Task<IDataResult<UserContractDetailDto>> GetBySessionId(Guid guid);
        Task<IDataResult<List<UserContractDetailDto>>> GetAll();
        Task<IDataResult<UserContractDto>> Add(UserContractDto dto);
        Task<IDataResult<UserContractDto>> Update(UserContractDto dto);
        Task<IDataResult<UserContractDto>> Delete(int id);
    }
}
