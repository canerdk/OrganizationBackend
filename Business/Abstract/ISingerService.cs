using Business.Utilities.Results;
using Entities.Dtos.Singer;

namespace Business.Abstract
{
    public interface ISingerService
    {
        Task<IDataResult<SingerDto>> GetById(int id);
        Task<IDataResult<List<SingerDto>>> GetAll();
        Task<IDataResult<SingerDto>> Add(SingerDto dto);
        Task<IDataResult<SingerDto>> Update(SingerDto dto);
        Task<IDataResult<SingerDto>> Delete(int id);
    }
}
