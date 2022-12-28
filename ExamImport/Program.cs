using ExamAPI.Models;
using ExamAPI.Services;
using System.Text.Json;

namespace ExamImport
{
    internal partial class Program
    {
        private static async Task Main(string[] args)
        {
            //var dbService = new DBService<Question>();

            //var lines = File.ReadAllLines(@"C:\Users\jpe190\Desktop\DVA-C01V23.0.txt");
            //var questionLines = splitIntoNumberedQuestions(lines);
            //var questions = questionLines.Select(ToQuestionLine).ToList();

            //string jsonString = JsonSerializer.Serialize(questions);
            //File.WriteAllText(@"c:\temp\questions.json", jsonString);

            //foreach (var question in questions) { await dbService.CreateAsync(question); }
        }

        private static Question ToQuestionLine(NumberedQuestion numberedQuestion)
        {
            var answerPrefixes = new List<string> { "A.", "B.", "C.", "D.", "E.", "F." };

            var question = new Question(numberedQuestion.QuestionNumber);
            foreach (var text in numberedQuestion.Lines)
            {
                var line = text.Trim();

                if (line.StartsWith("Answer"))
                {
                    var answerValues = line.Replace("Answer", "")
                        .Replace(":", " ")
                        .Replace(",", " ")
                        .Replace(".", " ")
                        .Replace("?", " ")
                        .Trim()
                        .Split(" ")
                        .ToList()
                        .Where(s => s != "")
                        .ToList();

                    bool foundAnswer = false;
                    for (int i = 0; i < answerValues.Count; i++)
                    {
                        if (answerValues[i].Length > 1)
                        {
                            break;
                        }

                        question.Answers.ForEach(a =>
                        {
                            if (a.AnswerLetter == answerValues[i].ToUpper())
                            {
                                a.IsCorrect = true;
                                foundAnswer = true;
                            }
                        });
                    }

                    if (!foundAnswer)
                    {
                    }

                    question.AnswerNotes.Add(line);
                }
                else if (question.AnswerNotes.Count > 0)
                {
                    question.AnswerNotes.Add(line);
                }
                else
                {
                    var isAnswer = answerPrefixes.Any(p => line.StartsWith(p));
                    if (isAnswer)
                    {
                        question.Answers.Add(new Answer(line));
                    }
                    else
                    {
                        if (question.Answers.Any())
                        {
                            question.Answers.Last().Text += " " + line;
                        }
                        else
                        {
                            question.Text.Add(line);
                        }
                    }
                }
            }

            if (question.Answers.Count == 0 || question.AnswerNotes.Count == 0)
            {
            }

            return question;
        }

        private static List<NumberedQuestion> splitIntoNumberedQuestions(string[] lines)
        {
            var nextQuestionNumber = 1;
            var questions = new List<NumberedQuestion>();
            var currentQuestion = new NumberedQuestion(nextQuestionNumber - 1);
            questions.Add(currentQuestion);

            foreach (var line in lines)
            {
                if (line.StartsWith($"{nextQuestionNumber}."))
                {
                    nextQuestionNumber++;
                    currentQuestion = new NumberedQuestion(nextQuestionNumber - 1);
                    questions.Add(currentQuestion);
                }
                currentQuestion.Lines.Add(line);
            }
            return questions;
        }
    }
}