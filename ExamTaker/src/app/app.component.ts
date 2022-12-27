import { Component } from '@angular/core';
import { ApiService } from '../api/api.service';

enum AnswerResult {
  None = 1,
  Correct = 2,
  Wrong = 3,
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  private static endpointUrl: string = 'api/Question';
  questions: Question[] = [];
  question!: Question;
  questionId: number = 0;
  percentageCorrect(): number {
    return (
      this.correctAnswers /
      (this.questionId == 1 ? 1 : this.questionId - 1)
    );
  }

  answerResult: AnswerResult = AnswerResult.None;
  AnswerResultType = AnswerResult;
  correctAnswers: number = 0;

  constructor(private readonly api: ApiService) {
    this.api.getAll<Question>(AppComponent.endpointUrl).subscribe((r) => {
      this.questions = r;
      this.NextQuestion();
    });
  }

  NextQuestion() {
    this.answerResult = AnswerResult.None;
    this.questionId++;
    this.question = this.questions[this.questionId];
    this.question.Answers.forEach((a) => (a.selected = false));
  }

  OnClickAnswer(answer: Answer) {
    answer.selected = !answer.selected;

    if (
      this.question.Answers.filter((a) => a.selected).length ==
      this.question.Answers.filter((a) => a.IsCorrect).length
    ) {
      this.CheckAnswers();
    }
  }

  CheckAnswers() {
    let incorrectAnswers = this.question.Answers.filter(
      (a) => a.selected != a.IsCorrect
    ).length;

    if (!incorrectAnswers) {
      this.answerResult = AnswerResult.Correct;
      this.correctAnswers++;
      setTimeout(() => {
        this.NextQuestion();
      }, 2000);
    } else {
      this.answerResult = AnswerResult.Wrong;
    }
  }
}

interface Question {
  QuestionNumber: number;
  Id: string;
  Text: string[];
  AnswerNotes: string[];
  Answers: Answer[];
}

interface Answer {
  AnswerLetter: string;
  Text: string;
  IsCorrect: boolean;
  selected: boolean;
}
