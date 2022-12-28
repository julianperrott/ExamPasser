using ExamAPI.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamAPI.Models;

public class Session : IDataAccessObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public int SessionNumber { get; set; }

    public int QuestionNumber { get; set; }

    public int CorrectAnswers { get; set; }
}