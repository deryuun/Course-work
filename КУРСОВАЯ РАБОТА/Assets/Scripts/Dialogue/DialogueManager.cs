using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerInputManager _manager; 
    [SerializeField] public TaskManager taskManager;
    
    [Header("Params")] 
    [SerializeField] private float typingSpeed = 0.00f;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject tasksPanel;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private GameObject continueIcon;
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] _choicesText;
    
    private Story _currentStory;

    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager _instance;

    private Coroutine _displayLineCoroutine;
    private bool _continueNextLine = false;

    private void Awake()
    {
        if (_instance != null)       
        {
            Debug.LogWarning("Error");
        }
        _instance = this;
        _manager = player.GetComponent<PlayerInputManager>();
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        
        _choicesText = new TextMeshProUGUI[choices.Length];
        var index = 0;
        foreach (var choice in choices) 
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update() 
    {
        if (!dialogueIsPlaying) 
        {
            return;
        }

        if (_continueNextLine 
            && _currentStory.currentChoices.Count == 0 
            && _manager.GetSubmitPressed())
        {
            ContinueStory();
        }
    }
    
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        
        ContinueStory();
    }
    
    private void ExitDialogueMode() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    
    private void ContinueStory() 
    {
        if (_currentStory.canContinue)
        {
            if (_displayLineCoroutine != null)
            {
                StopCoroutine(_displayLineCoroutine);
            }
            _displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue()));
            HandleTags(_currentStory.currentTags);
        } 
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        _continueNextLine = false;
        continueIcon.SetActive(false);
        HideChoices();

        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueIcon.SetActive(true);
        DisplayChoices();
        _continueNextLine = true;
    }

    private void HideChoices()
    {
        foreach (var choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentStoryCurrentTags)
    {
        foreach (var tag in currentStoryCurrentTags)
        {
            string[] splittedTag = tag.Split(':');
            if (splittedTag.Length == 2)
            {
                string key = splittedTag[0];
                string value = splittedTag[1];
            
                if (key == "speaker")
                {
                    speakerNameText.text = value;
                    speakerPortrait.overrideSprite = Resources.Load<Sprite>($"Portraits/{value}/main");
                } 
                else if (value == "showTasks")
                {
                    ShowTasks();
                }
            }
            else
            {
                Debug.LogWarning($"Incorrect tag - {tag}");
            }
        }
    }
    
    private void ShowTasks()
    {
        tasksPanel.SetActive(true);
        taskManager.isTaskPanelOpen = true;
        dialoguePanel.SetActive(false); 
    }

    public void CloseTasksPanel()
    {
        taskManager.isTaskPanelOpen = false;
        tasksPanel.SetActive(false);
        dialogueIsPlaying = false;
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = _currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too much choices");
        }

        int index = 0;
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }
        
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (_continueNextLine)
        {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            _manager.RegisterSubmitPressed();
            ContinueStory();
        }
    }
}
