using Business.Utilities.Results;
using Entities.Dtos.Event;

namespace Business.Abstract
{
    public interface IEventService
    {
        Task<IDataResult<EventDto>> GetById(int id);
        Task<IDataResult<List<EventDto>>> GetAll();
        Task<IDataResult<EventDto>> Add(EventDto dto);
        Task<IDataResult<EventDto>> Update(EventDto dto);
        Task<IDataResult<EventDto>> Delete(int id);
    }
}
