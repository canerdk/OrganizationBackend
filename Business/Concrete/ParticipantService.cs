using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Participant;
using Entities.Entities;

namespace Business.Concrete
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantDAL _participantDAL;
        private readonly IMapper _mapper;

        public ParticipantService(IParticipantDAL participantDAL, IMapper mapper)
        {
            _participantDAL = participantDAL;
            _mapper = mapper;
        }

        public async Task<IDataResult<ParticipantDto>> Add(ParticipantDto dto)
        {
            var result = await _participantDAL.AddAsync(_mapper.Map<Participant>(dto));

            if (result != null)
                return new SuccessDataResult<ParticipantDto>(_mapper.Map<ParticipantDto>(result));

            return new ErrorDataResult<ParticipantDto>();
        }

        public async Task<IDataResult<ParticipantDto>> Delete(int id)
        {
            var result = await _participantDAL.SoftDeleteAsync(id);

            if (result != null)
                return new SuccessDataResult<ParticipantDto>(_mapper.Map<ParticipantDto>(result));

            return new ErrorDataResult<ParticipantDto>();
        }

        public async Task<IDataResult<List<ParticipantDto>>> GetAll()
        {
            var results = await _participantDAL.GetAllAsync(x => !x.IsDeleted);

            if (results != null && results.Any())
                return new SuccessDataResult<List<ParticipantDto>>(_mapper.Map<List<ParticipantDto>>(results));

            return new ErrorDataResult<List<ParticipantDto>>();
        }

        public async Task<IDataResult<ParticipantDto>> GetById(int id)
        {
            var result = await _participantDAL.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (result != null)
                return new SuccessDataResult<ParticipantDto>(_mapper.Map<ParticipantDto>(result));

            return new ErrorDataResult<ParticipantDto>();
        }

        public async Task<IDataResult<ParticipantDto>> Update(ParticipantDto dto)
        {
            var exist = await _participantDAL.GetAsync(x => x.Id == dto.Id && !x.IsDeleted);

            if (exist != null)
            {
                var result = await _participantDAL.UpdateAsync(_mapper.Map(dto, exist));
                return new SuccessDataResult<ParticipantDto>(_mapper.Map<ParticipantDto>(result));
            }

            return new ErrorDataResult<ParticipantDto>();
        }
    }
}
