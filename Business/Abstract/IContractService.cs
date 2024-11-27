using Business.Utilities.Results;
using Entities.Dtos.Contract;

namespace Business.Abstract
{
    public interface IContractService
    {
        Task<IDataResult<ContractDto>> GetById(int id);
        Task<IDataResult<List<ContractDto>>> GetAll();
        Task<IDataResult<ContractDto>> Add(ContractDto dto);
        Task<IDataResult<ContractDto>> Update(ContractDto dto);
        Task<IDataResult<ContractDto>> Delete(int id);
    }
}
