using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using Entities.Dtos.Singer;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class SingerService : ISingerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public SingerService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IDataResult<SingerDto>> Add(SingerDto dto)
        {
            //var result = await _eventDAL.AddAsync(_mapper.Map<Event>(dto));

            //if (result != null)
            //    return new SuccessDataResult<SingerDto>(_mapper.Map<SingerDto>(result));

            return new ErrorDataResult<SingerDto>();
        }

        public async Task<IDataResult<SingerDto>> Delete(int id)
        {
            var exist = await _userManager.Users.FirstOrDefaultAsync(x => x.Type == 2 && x.Id == id);

            if(exist == null)
                new ErrorDataResult<SingerDto>("Solist bulunamadı.");

            var result = await _userManager.DeleteAsync(exist);

            if (result.Succeeded)
                return new SuccessDataResult<SingerDto>(_mapper.Map<SingerDto>(result));

            return new ErrorDataResult<SingerDto>();
        }

        public async Task<IDataResult<List<SingerDto>>> GetAll()
        {
            var results = await _userManager.Users.Where(x => x.Type == 2).ToListAsync();

            if (results != null && results.Any())
                return new SuccessDataResult<List<SingerDto>>(_mapper.Map<List<SingerDto>>(results));

            return new ErrorDataResult<List<SingerDto>>();
        }

        public async Task<IDataResult<SingerDto>> GetById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Type == 2 && x.Id == id);

            if (result != null)
                return new SuccessDataResult<SingerDto>(_mapper.Map<SingerDto>(result));

            return new ErrorDataResult<SingerDto>();
        }

        public async Task<IDataResult<SingerDto>> Update(SingerDto dto)
        {
            var exist = await _userManager.Users.FirstOrDefaultAsync(x => x.Type == 2 && x.Id == dto.Id);

            if (exist != null)
            {
                var result = await _userManager.UpdateAsync(_mapper.Map(dto, exist));
                return new SuccessDataResult<SingerDto>(_mapper.Map<SingerDto>(result));
            }

            return new ErrorDataResult<SingerDto>();
        }
    }
}
