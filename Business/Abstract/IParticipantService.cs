using Business.Utilities.Results;
using Entities.Dtos.Participant;

namespace Business.Abstract
{
    public interface IParticipantService
    {
        Task<IDataResult<ParticipantDto>> GetById(int id);
        Task<IDataResult<List<ParticipantDto>>> GetAll();
        Task<IDataResult<ParticipantDto>> Add(ParticipantDto dto);
        Task<IDataResult<ParticipantDto>> Update(ParticipantDto dto);
        Task<IDataResult<ParticipantDto>> Delete(int id);
    }
}
