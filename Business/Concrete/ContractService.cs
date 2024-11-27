using AutoMapper;
using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Contract;
using Entities.Entities;

namespace Business.Concrete
{
    public class ContractService : IContractService
    {
        private readonly IContractDAL _contractDAL;
        private readonly IMapper _mapper;

        public ContractService(IContractDAL contractDAL, IMapper mapper)
        {
            _contractDAL = contractDAL;
            _mapper = mapper;
        }

        public async Task<IDataResult<ContractDto>> Add(ContractDto dto)
        {
            var result = await _contractDAL.AddAsync(_mapper.Map<Contract>(dto));

            if(result != null)
                return new SuccessDataResult<ContractDto>(_mapper.Map<ContractDto>(result));

            return new ErrorDataResult<ContractDto>();
        }

        public async Task<IDataResult<ContractDto>> Delete(int id)
        {
            var result =  await _contractDAL.SoftDeleteAsync(id);

            if (result != null)
                return new SuccessDataResult<ContractDto>(_mapper.Map<ContractDto>(result));

            return new ErrorDataResult<ContractDto>();
        }

        public async Task<IDataResult<List<ContractDto>>> GetAll()
        {
            var results = await _contractDAL.GetAllAsync(x => !x.IsDeleted);

            if (results != null && results.Any())
                return new SuccessDataResult<List<ContractDto>>(_mapper.Map<List<ContractDto>>(results));

            return new ErrorDataResult<List<ContractDto>>();
        }

        public async Task<IDataResult<ContractDto>> GetById(int id)
        {
            var result = await _contractDAL.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (result != null)
                return new SuccessDataResult<ContractDto>(_mapper.Map<ContractDto>(result));

            return new ErrorDataResult<ContractDto>();
        }

        public async Task<IDataResult<ContractDto>> Update(ContractDto dto)
        {
            var exist = await _contractDAL.GetAsync(x => x.Id == dto.Id && !x.IsDeleted);

            if (exist != null)
            {
                var result = await _contractDAL.UpdateAsync(_mapper.Map(dto, exist));
                return new SuccessDataResult<ContractDto>(_mapper.Map<ContractDto>(result));
            }

            return new ErrorDataResult<ContractDto>();
        }
    }
}
