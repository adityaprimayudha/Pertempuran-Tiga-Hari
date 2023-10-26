using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;


public class SwitchMode : MonoBehaviour
{
    [SerializeField] private Sprite spriteCasual;
    [SerializeField] private Sprite spriteBattle;
    [SerializeField] private RuntimeAnimatorController animatorCasual;
    [SerializeField] private RuntimeAnimatorController animatorBattle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthBar;

    void OnEnable()
    {
        Lua.RegisterFunction(nameof(SwitchToCasual), this, SymbolExtensions.GetMethodInfo(() => SwitchToCasual()));
        Lua.RegisterFunction(nameof(SwitchToBattle), this, SymbolExtensions.GetMethodInfo(() => SwitchToBattle()));
        Lua.RegisterFunction(nameof(TimeScale), this, SymbolExtensions.GetMethodInfo(() => TimeScale()));
        Lua.RegisterFunction(nameof(NextScene), this, SymbolExtensions.GetMethodInfo(() => NextScene("")));
    }

    public void SwitchToCasual()
    {
        player.GetComponent<SpriteRenderer>().sprite = spriteCasual;
        player.GetComponent<Animator>().runtimeAnimatorController = animatorCasual;
        if (healthBar != null)
        {
            healthBar.SetActive(false);
        }
    }

    public void SwitchToBattle()
    {
        player.GetComponent<SpriteRenderer>().sprite = spriteBattle;
        player.GetComponent<Animator>().runtimeAnimatorController = animatorBattle;
        if (healthBar != null)
        {
            healthBar.SetActive(true);
        }
    }
    public void TimeScale()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        else if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
