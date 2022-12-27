using ExamAPI.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamAPI.Models;

public class Question : IDataAccessObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public Question(int questionNumber)
    {
        this.QuestionNumber = questionNumber;
    }

    public int QuestionNumber { get; set; }

    public List<string> Text { get; set; } = new List<string>();

    public List<Answer> Answers { get; set; } = new List<Answer>();

    public List<string> AnswerNotes { get; set; } = new List<string>();
}