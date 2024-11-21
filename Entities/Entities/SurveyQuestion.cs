using Entities.Common;

namespace Entities.Entities
{
    public class SurveyQuestion : BaseEntity
    {
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int SurveyId { get; set; }
    }
}
