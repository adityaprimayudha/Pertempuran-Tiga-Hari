using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupQuiz : MonoBehaviour
{
    public GameObject background;
    public TextMeshProUGUI question;
    public Button[] buttons;
    private QuizData quiz;
    public string typeofquiz;
    private int currentQuiz = 0;
    private int correctAnswer;
    [SerializeField] private GameObject correctPanel;
    [SerializeField] private GameObject incorrectPanel;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject finishButton;

    private void Start()
    {
        quiz = QuizManager.GetInstance().GetQuiz(typeofquiz);
        InsertQuizzes();
    }

    public void InsertQuizzes()
    {
        question.text = quiz.quizzes[currentQuiz].question;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = quiz.quizzes[currentQuiz].answers[i];
        }
        correctAnswer = quiz.quizzes[currentQuiz].correctAnswer;
    }

    public void CheckAnswer(int answer)
    {
        if (answer == correctAnswer)
        {
            Debug.Log("Correct");
            QuizManager.GetInstance().totalCorrectAnswer++;
            correctPanel.SetActive(true);
            ButtonCheck();
        }
        else
        {
            Debug.Log("Wrong");
            QuizManager.GetInstance().totalIncorrectAnswer++;
            incorrectPanel.SetActive(true);
            ButtonCheck();
        }
    }
    private void ButtonCheck()
    {
        if (currentQuiz == quiz.quizzes.Length - 1)
        {
            nextButton.SetActive(false);
            finishButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            finishButton.SetActive(false);
        }
    }
    public void CorrectAnswer()
    {
        correctPanel.SetActive(false);
        NextQuiz();
    }

    public void IncorrectAnswer()
    {
        incorrectPanel.SetActive(false);
        NextQuiz();
    }

    public void NextQuiz()
    {
        currentQuiz++;
        correctPanel.SetActive(false);
        incorrectPanel.SetActive(false);
        nextButton.SetActive(false);
    }
    public void FinishQuiz(string sceneName)
    {
        background.SetActive(false);
        currentQuiz = 0;
        Debug.Log("Correct Answer: " + QuizManager.GetInstance().totalCorrectAnswer + "\nIncorrect Answer: " + QuizManager.GetInstance().totalIncorrectAnswer);
        SceneManager.LoadScene(sceneName);
    }
}
