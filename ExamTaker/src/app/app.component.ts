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
  private static questionEndpointUrl: string = 'api/Question';
  private static userAnswerEndpointUrl: string = 'api/UserAnswer';
  private static sessionEndpointUrl: string = 'api/Session';
  questions: Question[] = [];
  question: Question = {
    QuestionNumber: 1,
    Id: '',
    Text: [],
    AnswerNotes: [],
    Answers: [],
  };
  percentageCorrect(): number {
    return (
      this.session.CorrectAnswers /
      (this.session.QuestionNumber == 1 ? 1 : this.session.QuestionNumber - 1)
    );
  }
  session: Session = {
    Id: '',
    SessionNumber: 1,
    QuestionNumber: 0,
    CorrectAnswers: 0,
  };
  answerResult: AnswerResult = AnswerResult.None;
  AnswerResultType = AnswerResult;

  constructor(private readonly api: ApiService) {
    this.api
      .getAll<Session>(AppComponent.sessionEndpointUrl)
      .subscribe((sessions) => {
        if (sessions.length > 0) {
          sessions.sort((s1, s2) => s2.SessionNumber - s1.SessionNumber);
          this.session = sessions[0];
        } else {
          this.api
            .post(AppComponent.sessionEndpointUrl, this.session)
            .subscribe((r) => (this.session = r));
        }

        this.api
          .getAll<Question>(AppComponent.questionEndpointUrl)
          .subscribe((r) => {
            this.questions = r;
            this.NextQuestion();
          });
      });
  }

  NextQuestion() {
    this.api
      .put(AppComponent.sessionEndpointUrl, this.session)
      .subscribe((r) => {});

    this.answerResult = AnswerResult.None;
    this.session.QuestionNumber++;
    this.question = this.questions[this.session.QuestionNumber];
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

  NewSession() {
    var newSession: Session = {
      Id: '',
      SessionNumber: this.session.SessionNumber + 1,
      QuestionNumber: 0,
      CorrectAnswers: 0,
    };

    this.api
      .post(AppComponent.sessionEndpointUrl, newSession)
      .subscribe((r) => {
        this.session = r;
        this.NextQuestion();
      });
  }

  CheckAnswers() {
    let userAnswer: UserAnswer = {
      Id: '',
      QuestionNumber: this.question.QuestionNumber,
      Answers: this.question.Answers.filter((q) => q.selected).map(
        (s) => s.AnswerLetter
      ),
      IsCorrect:
        this.question.Answers.filter((a) => a.selected != a.IsCorrect).length ==
        0,
    };

    if (userAnswer.IsCorrect) {
      this.answerResult = AnswerResult.Correct;
      this.session.CorrectAnswers++;
      setTimeout(() => {
        this.NextQuestion();
      }, 2000);
    } else {
      this.answerResult = AnswerResult.Wrong;
    }

    this.api
      .post(AppComponent.userAnswerEndpointUrl, userAnswer)
      .subscribe((r) => {});
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

interface UserAnswer {
  Id: string;
  QuestionNumber: number;
  Answers: string[];
  IsCorrect: boolean;
}

interface Session {
  Id: string;
  SessionNumber: number;
  QuestionNumber: number;
  CorrectAnswers: number;
}
