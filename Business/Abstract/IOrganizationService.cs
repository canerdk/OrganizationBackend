using Business.Utilities.Results;
using Entities.Dtos.Organization;

namespace Business.Abstract
{
    public interface IOrganizationService
    {
        Task<IDataResult<OrganizationDto>> GetById(int id);
        Task<IDataResult<List<OrganizationDto>>> GetAll();
        Task<IDataResult<OrganizationDto>> Add(OrganizationDto dto);
        Task<IDataResult<OrganizationDto>> Update(OrganizationDto dto);
        Task<IDataResult<OrganizationDto>> Delete(int id);
    }
}
