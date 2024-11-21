using Entities.Common;

namespace Entities.Entities
{
    public class SurveyResponse : BaseEntity
    {
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public ICollection<SurveyResponseAnswer> Answers { get; set; }
    }
}
