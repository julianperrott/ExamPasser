using ExamAPI.Models;
using ExamAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : AppController<Question>
{
    public QuestionController(DBService<Question> service) => _dbService = service;
}