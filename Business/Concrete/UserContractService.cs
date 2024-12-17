using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.UserContract;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class UserContractService : IUserContractService
    {
        private readonly IUserContractDAL _userContractDAL;
        private readonly IMapper _mapper;

        public UserContractService(IUserContractDAL userContractDAL, IMapper mapper)
        {
            _userContractDAL = userContractDAL;
            _mapper = mapper;
        }

        public async Task<IDataResult<UserContractDto>> Add(UserContractDto dto)
        {
            dto.SessionId = Guid.NewGuid();
            dto.Date = DateTime.Now;
            var result = await _userContractDAL.AddAsync(_mapper.Map<UserContract>(dto));

            if (result != null)
                return new SuccessDataResult<UserContractDto>(_mapper.Map<UserContractDto>(result));

            return new ErrorDataResult<UserContractDto>();
        }

        public async Task<IDataResult<UserContractDto>> Delete(int id)
        {
            var result = await _userContractDAL.SoftDeleteAsync(id);

            if (result != null)
                return new SuccessDataResult<UserContractDto>(_mapper.Map<UserContractDto>(result));

            return new ErrorDataResult<UserContractDto>();
        }

        public async Task<IDataResult<List<UserContractDetailDto>>> GetAll()
        {
            var results = await _userContractDAL.GetAllAsync(x => !x.IsDeleted, include: i => i.Include(x => x.Contract).Include(x => x.Event).Include(x => x.User));

            if (results != null && results.Any())
                return new SuccessDataResult<List<UserContractDetailDto>>(_mapper.Map<List<UserContractDetailDto>>(results));

            return new ErrorDataResult<List<UserContractDetailDto>>();
        }

        public async Task<IDataResult<UserContractDto>> GetById(int id)
        {
            var result = await _userContractDAL.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (result != null)
                return new SuccessDataResult<UserContractDto>(_mapper.Map<UserContractDto>(result));

            return new ErrorDataResult<UserContractDto>();
        }

        public async Task<IDataResult<UserContractDetailDto>> GetBySessionId(Guid guid)
        {
            var result = await _userContractDAL.GetAsync(x => x.SessionId == guid && !x.IsDeleted, include: i => i.Include(x => x.Contract).Include(x => x.Event).Include(x => x.User));

            if (result != null)
                return new SuccessDataResult<UserContractDetailDto>(_mapper.Map<UserContractDetailDto>(result));

            return new ErrorDataResult<UserContractDetailDto>();
        }

        public async Task<IDataResult<UserContractDto>> Update(UserContractDto dto)
        {
            var exist = await _userContractDAL.GetAsync(x => x.Id == dto.Id && !x.IsDeleted);

            if (exist != null)
            {
                var result = await _userContractDAL.UpdateAsync(_mapper.Map(dto, exist));
                return new SuccessDataResult<UserContractDto>(_mapper.Map<UserContractDto>(result));
            }

            return new ErrorDataResult<UserContractDto>();
        }
    }
}
