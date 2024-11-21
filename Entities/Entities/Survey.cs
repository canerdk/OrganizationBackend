using Entities.Common;

namespace Entities.Entities
{
    public class Survey : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();
    }
}
