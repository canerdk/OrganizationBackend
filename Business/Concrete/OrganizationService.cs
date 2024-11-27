using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Organization;
using Entities.Entities;

namespace Business.Concrete
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationDAL _organizationDAL;
        private readonly IMapper _mapper;

        public OrganizationService(IOrganizationDAL organizationDAL, IMapper mapper)
        {
            _organizationDAL = organizationDAL;
            _mapper = mapper;
        }

        public async Task<IDataResult<OrganizationDto>> Add(OrganizationDto dto)
        {
            var result = await _organizationDAL.AddAsync(_mapper.Map<Organization>(dto));

            if (result != null)
                return new SuccessDataResult<OrganizationDto>(_mapper.Map<OrganizationDto>(result));

            return new ErrorDataResult<OrganizationDto>();
        }

        public async Task<IDataResult<OrganizationDto>> Delete(int id)
        {
            var result = await _organizationDAL.SoftDeleteAsync(id);

            if (result != null)
                return new SuccessDataResult<OrganizationDto>(_mapper.Map<OrganizationDto>(result));

            return new ErrorDataResult<OrganizationDto>();
        }

        public async Task<IDataResult<List<OrganizationDto>>> GetAll()
        {
            var results = await _organizationDAL.GetAllAsync(x => !x.IsDeleted);

            if (results != null && results.Any())
                return new SuccessDataResult<List<OrganizationDto>>(_mapper.Map<List<OrganizationDto>>(results));

            return new ErrorDataResult<List<OrganizationDto>>();
        }

        public async Task<IDataResult<OrganizationDto>> GetById(int id)
        {
            var result = await _organizationDAL.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (result != null)
                return new SuccessDataResult<OrganizationDto>(_mapper.Map<OrganizationDto>(result));

            return new ErrorDataResult<OrganizationDto>();
        }

        public async Task<IDataResult<OrganizationDto>> Update(OrganizationDto dto)
        {
            var exist = await _organizationDAL.GetAsync(x => x.Id == dto.Id && !x.IsDeleted);

            if (exist != null)
            {
                var result = await _organizationDAL.UpdateAsync(_mapper.Map(dto, exist));
                return new SuccessDataResult<OrganizationDto>(_mapper.Map<OrganizationDto>(result));
            }

            return new ErrorDataResult<OrganizationDto>();
        }
    }
}
