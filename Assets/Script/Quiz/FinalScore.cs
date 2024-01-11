using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI correctAnswer;
    [SerializeField] private TextMeshProUGUI kesimpulan;
    private int totalCorrectAnswers;
    private int totalQuiz;
    private float percentage;

    void Start()
    {
        totalQuiz = QuizManager.GetInstance().totalCorrectAnswer + QuizManager.GetInstance().totalIncorrectAnswer;
        correctAnswer.text = "Total jawaban benar : " + QuizManager.GetInstance().totalCorrectAnswer + "/" + totalQuiz;
        CheckIndicator();
    }

    public void CheckIndicator()
    {
        totalCorrectAnswers = QuizManager.GetInstance().totalCorrectAnswer;
        percentage = (float)totalCorrectAnswers / (float)totalQuiz * 100;
        Debug.Log("total correct :" + totalCorrectAnswers + "\npercentage :" + percentage);
        if (percentage >= 50)
        {
            kesimpulan.text = "Selamat Anda telah menyelesaikan gim\n<color=#66313C>PERTEMPURAN TIGA HARI SURABAYA</color=#66313C>\ndan berhasil menyelesaikan soal dengan\n<color=#059D00>BAIK</color>";
        }
        else
        {
            kesimpulan.text = "Selamat Anda telah menyelesaikan gim\n<color=#66313C>PERTEMPURAN TIGA HARI SURABAYA</color=#66313C>\ntetapi disarankan untuk <color=red>MENGULANGI</color> agar pemahaman lebih baik";
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
