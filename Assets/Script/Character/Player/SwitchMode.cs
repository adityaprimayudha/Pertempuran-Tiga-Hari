using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEditor.Animations;
using UnityEngine.SceneManagement;

public class SwitchMode : MonoBehaviour
{
    [SerializeField] private Sprite spriteCasual;
    [SerializeField] private Sprite spriteBattle;
    [SerializeField] private AnimatorController animatorCasual;
    [SerializeField] private AnimatorController animatorBattle;
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
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteCasual;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorCasual;
        if (healthBar != null)
        {
            healthBar.SetActive(false);
        }
    }

    public void SwitchToBattle()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBattle;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorBattle;
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
