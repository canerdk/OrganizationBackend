using Entities.Common;

namespace Entities.Dtos.Contract
{
    public class ContractDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
