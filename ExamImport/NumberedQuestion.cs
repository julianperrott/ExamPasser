namespace ExamImport
{
    internal partial class Program
    {
        public class NumberedQuestion
        {
            public NumberedQuestion(int questionNumber)
            {
                QuestionNumber = questionNumber;
            }

            public int QuestionNumber { get; set; }

            public List<string> Lines { get; set; } = new List<string>();
        }
    }
}