using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    // For load and save variables on ink
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dimmedBackground;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI displayNameText;
    //[SerializeField] private Animator portraitAnimator;
    //private Animator layoutAnimator;

    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    private bool canContinueToNextLine = false;
    private bool hasChosenChoice = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";

    private DialogueVariables dialogueVariables;
    private InkExternalFunctions inkExternalFunctions;

    public event Action OnDialogueStarted;
    public event Action OnDialogueLineDisplay;
    public event Action OnDialogueEnded;
    public bool IsDialoguePlaying { get; private set; }

    private Dictionary<string, Action<string>> externalFunctions = new Dictionary<string, Action<string>>();


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Dialogue Manager in the scene.");
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        inkExternalFunctions = new InkExternalFunctions();
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dimmedBackground.SetActive(false);
        dialoguePanel.SetActive(false);
        IsDialoguePlaying = false;

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        // layoutAnimator = dialoguePanel.GetComponent<Animator>();

    }

    private void Update()
    {
        if (!IsDialoguePlaying)
        {
            return;
        }
        if (InputManager.GetInstance().GetInteractPressed()
            && canContinueToNextLine
            && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    public DialogueManager EnterDialogueMode(TextAsset inkJSON, Animator emoteAnimator = null)
    {
        if (IsDialoguePlaying || inkJSON == null)
        {
            Debug.Log("KOSONG ANJ");
            Debug.LogWarning("Dialogue is already playing or inkJSON is null");
            return null;
        }

        currentStory = new Story(inkJSON.text);
        dimmedBackground.SetActive(true);
        dialoguePanel.SetActive(true);
        IsDialoguePlaying = true;

        dialogueVariables.StartListening(currentStory);
        if (emoteAnimator != null)
        {
            inkExternalFunctions.Bind(currentStory, emoteAnimator);
        }
        foreach (var externalFunction in externalFunctions)
        {
            currentStory.BindExternalFunction(externalFunction.Key, externalFunction.Value);
        }

        // reset portrait, layout, and speaker as default
        displayNameText.text = "???";
        //portraitAnimator.Play("default");
        //portraitAnimator.transform.parent.gameObject.SetActive(true);

        //layoutAnimator.Play("default");

        OnDialogueStarted?.Invoke();


        ContinueStory();
        return instance;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            // otherwise, handle the normal case for continuing the story
            else
            {
                // handle tags
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        dialogueVariables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);

        foreach (var externalFunction in externalFunctions)
        {
            if (currentStory.TryGetExternalFunction(externalFunction.Key, out var ext))
            {
                currentStory.UnbindExternalFunction(externalFunction.Key);
            }
        }
        externalFunctions.Clear();

        IsDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        dimmedBackground.SetActive(false);

        OnDialogueEnded?.Invoke();
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("There are more choices than there are choice buttons." + currentChoices.Count);
        }
        else if (currentChoices.Count > 0)
        {
            continueIcon.SetActive(false);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
        //EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        // NOTE: The below two lines were added to fix a bug after the Youtube video was made
        InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
        ContinueStory();
    }

    private IEnumerator DisplayLine(string line)
    {
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        OnDialogueLineDisplay?.Invoke();
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                if (hasChosenChoice)
                {
                    hasChosenChoice = false;
                    continue;
                }
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }
    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(" ");
            List<string> newSplitTag = new List<string>(splitTag.Length);

            for (int i = 0; i < splitTag.Length; i++)
            {
                //Check if curreent subString is one of defined tag, if yes continue
                if (splitTag[i].Contains(SPEAKER_TAG) || splitTag[i].Contains(PORTRAIT_TAG) || splitTag[i].Contains(LAYOUT_TAG) || splitTag[i].Contains(AUDIO_TAG))
                {
                    newSplitTag.Add(splitTag[i]);
                    continue;
                }
                else
                {
                    //Merge the tag with the previous sub split tag in the list
                    newSplitTag[newSplitTag.Count - 1] += " " + splitTag[i];
                }
            }

            foreach (string remainingTag in newSplitTag)
            {
                ProcessTag(remainingTag.Split(':'));
            }
            // parse the tag
            // string[] splitTag = tag.Split(':');
            // if(splitTag.Length == 2)
            // {
            //     ProcessTag(splitTag);
            // }
            // else if(tag.Contains(" "))
            // {
            //     foreach(string remainingTag in tag.Split(' '))
            //     {
            //         ProcessTag(remainingTag.Split(':'));
            //     }
            // }
        }

        void ProcessTag(string[] splitTag)
        {
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    if (tagValue.Equals("null"))
                    {
                        displayNameText.transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        displayNameText.transform.parent.gameObject.SetActive(true);
                        displayNameText.text = tagValue;
                    }
                    break;
                // case PORTRAIT_TAG:
                //     if (tagValue.Equals("null"))
                //     {
                //         portraitAnimator.transform.parent.gameObject.SetActive(false);
                //     }
                //     else
                //     {
                //         portraitAnimator.transform.parent.gameObject.SetActive(true);
                //         portraitAnimator.Play(tagValue);
                //     }
                //     break;
                // case LAYOUT_TAG:
                //     layoutAnimator.Play(tagValue);
                //     break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }
    /// <summary>
    /// Get the current state of a variable in ink<br/>
    /// Useful for checking a variable before preparing for dialogue
    /// </summary>
    /// <param name="variableName">The name of the variable to get</param>
    /// <returns>A variable from ink story. <br/>
    /// To compare the value, use ToInt(), ToFloat(), ToString(), or ToBool() <br/>
    /// </returns>
    // public Ink.Runtime.Object GetVariableState(string variableName)
    // {
    //     Ink.Runtime.Object variableValue = null;
    //     dialogueVariables.variables.TryGetValue(variableName, out variableValue);
    //     if (variableValue == null)
    //     {
    //         Debug.LogWarning("Ink Variable was found to be null: " + variableName);
    //     }
    //     return variableValue;
    // }

    /// <summary>
    /// Set the a variable state of a variable in ink <br />
    /// Usage: GetInstance().SetVariableState()["variableName"] = value;
    /// </summary>
    /// <returns>VariableState, This function cant be chained</returns>
    // public VariablesState SetVariableState()
    // {
    //     return currentStory.state.variablesState;
    // }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    // public void OnApplicationQuit()
    // {
    //     dialogueVariables.SaveVariables();
    // }
}
