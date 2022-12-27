namespace ExamAPI.Models;

public class Answer
{
    public Answer(string text)
    {
        Text = text;
        AnswerLetter = text.Substring(0, 1).ToUpper();
    }

    public string AnswerLetter { get; set; }

    public string Text { get; set; }

    public bool IsCorrect { get; set; }
}