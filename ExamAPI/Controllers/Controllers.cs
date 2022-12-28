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

[ApiController]
[Route("api/[controller]")]
public class UserAnswerController : AppController<UserAnswer>
{
    public UserAnswerController(DBService<UserAnswer> service) => _dbService = service;
}

[ApiController]
[Route("api/[controller]")]
public class SessionController : AppController<Session>
{
    public SessionController(DBService<Session> service) => _dbService = service;
}