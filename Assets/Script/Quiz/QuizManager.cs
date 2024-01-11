using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    private static QuizManager instance;
    [HideInInspector]
    public int totalCorrectAnswer = 0;
    [HideInInspector]
    public int totalIncorrectAnswer = 0;
    public static QuizManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public QuizData GetQuiz(string typeofquiz)
    {
        //Editor Mode or Runtime Mode
        if (typeofquiz == "Pelabuhan")
        {
            Debug.Log("Quiz Pelabuhan");
            if (File.Exists(Application.dataPath + "/Script/Quiz/QuizPelabuhan.json"))
            {
                string json = File.ReadAllText(Application.dataPath + "/Script/Quiz/QuizPelabuhan.json");
                QuizData quiz = JsonConvert.DeserializeObject<QuizData>(json);
                return quiz;
            }
            else if (File.Exists(Directory.GetCurrentDirectory() + "\\Pertempuran Tiga Hari Surabaya_Data\\Quiz\\QuizPelabuhan.json"))
            {
                string json = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Pertempuran Tiga Hari Surabaya_Data\\Quiz\\QuizPelabuhan.json");
                QuizData quiz = JsonConvert.DeserializeObject<QuizData>(json);
                return quiz;
            }
            else
            {
                Debug.Log("Quiz Pelabuhan not found");
                return null;
            }
        }
        else if (typeofquiz == "Penyerbuan")
        {
            Debug.Log("Quiz Penyerbuan");
            if (File.Exists(Application.dataPath + "/Script/Quiz/QuizPenyerbuan.json"))
            {
                string json = File.ReadAllText(Application.dataPath + "/Script/Quiz/QuizPenyerbuan.json");
                QuizData quiz = JsonConvert.DeserializeObject<QuizData>(json);
                return quiz;
            }
            else if (File.Exists(Directory.GetCurrentDirectory() + "\\Pertempuran Tiga Hari Surabaya_Data\\Quiz\\QuizPenyerbuan.json"))
            {
                string json = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Pertempuran Tiga Hari Surabaya_Data\\Quiz\\QuizPenyerbuan.json");
                QuizData quiz = JsonConvert.DeserializeObject<QuizData>(json);
                return quiz;
            }
            else
            {
                Debug.Log("Quiz Penyerbuan not found");
                return null;
            }
        }
        else if (typeofquiz == "Final")
        {
            Debug.Log("Quiz Final");
            if (File.Exists(Application.dataPath + "/Script/Quiz/QuizFinal.json"))
            {
                string json = File.ReadAllText(Application.dataPath + "/Script/Quiz/QuizFinal.json");
                QuizData quiz = JsonConvert.DeserializeObject<QuizData>(json);
                return quiz;
            }
            else if (File.Exists(Directory.GetCurrentDirectory() + "\\Pertempuran Tiga Hari Surabaya_Data\\Quiz\\QuizFinal.json"))
            {
                string json = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Pertempuran Tiga Hari Surabaya_Data\\Quiz\\QuizFinal.json");
                QuizData quiz = JsonConvert.DeserializeObject<QuizData>(json);
                return quiz;
            }
            else
            {
                Debug.Log("Quiz Final not found");
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
