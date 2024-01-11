using System;
using UnityEngine;

[Serializable]
public class QuizData
{
    public Quiz[] quizzes;
}

[Serializable]
public class Quiz : MonoBehaviour
{
    public string question;
    public string[] answers;
    public int correctAnswer;

    public Quiz(string question, string[] answers, int correct)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswer = correct;
    }
}
