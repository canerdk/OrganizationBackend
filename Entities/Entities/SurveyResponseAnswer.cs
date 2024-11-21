using Entities.Common;

namespace Entities.Entities
{
    public class SurveyResponseAnswer : BaseEntity
    {
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
    }
}
