using ExamAPI.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamAPI.Models;

public class UserAnswer : IDataAccessObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public int QuestionNumber { get; set; }

    public List<string> Answers { get; set; } = new List<string>();

    public bool IsCorrect { get; set; }
}