using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Repository;
using Entities.Entities;

namespace DataAccess.Concrete
{
    public class ContractDAL : EntityRepository<Contract>, IContractDAL
    {
        public ContractDAL(AppDbContext context) : base(context)
        {
        }
    }
}
