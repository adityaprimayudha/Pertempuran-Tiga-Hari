using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class POST : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailLogin;
    [SerializeField] private TMP_InputField passwordLogin;
    [SerializeField] private TMP_InputField emailRegister;
    [SerializeField] private TMP_InputField passwordRegister;
    private string urlLogin = "https://reqres.in/api/login";
    private string urlRegister = "https://reqres.in/api/register";

    IEnumerator SendRegisterRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", emailRegister.text);
        form.AddField("password", passwordRegister.text);

        using (UnityWebRequest www = UnityWebRequest.Post(urlRegister, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    IEnumerator SendLoginRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", emailLogin.text);
        form.AddField("password", passwordLogin.text);

        using (UnityWebRequest www = UnityWebRequest.Post(urlLogin, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Login Success!!\nToken :" + JsonUtility.FromJson<Token>(www.downloadHandler.text).token);
            }
        }
    }

    public void Register()
    {
        StartCoroutine(SendRegisterRequest());
    }
    public void Login()
    {
        StartCoroutine(SendLoginRequest());
    }
    public class Token
    {
        public string token;
    }
}
