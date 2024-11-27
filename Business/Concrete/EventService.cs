using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Event;
using Entities.Entities;

namespace Business.Concrete
{
    public class EventService : IEventService
    {
        private readonly IEventDAL _eventDAL;
        private readonly IMapper _mapper;

        public EventService(IEventDAL eventDAL, IMapper mapper)
        {
            _eventDAL = eventDAL;
            _mapper = mapper;
        }

        public async Task<IDataResult<EventDto>> Add(EventDto dto)
        {
            var result = await _eventDAL.AddAsync(_mapper.Map<Event>(dto));

            if (result != null)
                return new SuccessDataResult<EventDto>(_mapper.Map<EventDto>(result));

            return new ErrorDataResult<EventDto>();
        }

        public async Task<IDataResult<EventDto>> Delete(int id)
        {
            var result = await _eventDAL.SoftDeleteAsync(id);

            if (result != null)
                return new SuccessDataResult<EventDto>(_mapper.Map<EventDto>(result));

            return new ErrorDataResult<EventDto>();
        }

        public async Task<IDataResult<List<EventDto>>> GetAll()
        {
            var results = await _eventDAL.GetAllAsync(x => !x.IsDeleted);

            if (results != null && results.Any())
                return new SuccessDataResult<List<EventDto>>(_mapper.Map<List<EventDto>>(results));

            return new ErrorDataResult<List<EventDto>>();
        }

        public async Task<IDataResult<EventDto>> GetById(int id)
        {
            var result = await _eventDAL.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (result != null)
                return new SuccessDataResult<EventDto>(_mapper.Map<EventDto>(result));

            return new ErrorDataResult<EventDto>();
        }

        public async Task<IDataResult<EventDto>> Update(EventDto dto)
        {
            var exist = await _eventDAL.GetAsync(x => x.Id == dto.Id && !x.IsDeleted);

            if (exist != null)
            {
                var result = await _eventDAL.UpdateAsync(_mapper.Map(dto, exist));
                return new SuccessDataResult<EventDto>(_mapper.Map<EventDto>(result));
            }

            return new ErrorDataResult<EventDto>();
        }
    }
}
