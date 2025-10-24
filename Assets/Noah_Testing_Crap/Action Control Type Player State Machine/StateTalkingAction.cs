using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = System.Random;

public class StateTalkingAction : MonoBehaviour, IControlTypeState
{
    [SerializeField] private InputActionReference inputClickOnThings;
    [SerializeField] private RectTransform sentenceContainerRectangle;
    [SerializeField] private RectTransform wordSelectorRectangle;
    [SerializeField] private GameObject emptyUIGameObject;
    private GameObject sentenceContainerPanel => sentenceContainerRectangle.gameObject;
    private GameObject wordSelectorPanel => wordSelectorRectangle.gameObject;
    private List<WordBehaviour> currentSentence;
    private List<WordBehaviour> currentWordsThatCanBeSelected;

    private float currentTimeInSeconds;

    private IConversable currentConversable;

    private void OnEnable()
    {
        inputClickOnThings.action.Enable();
        inputClickOnThings.action.performed += OnInputActionClickOnThings;
        
        currentSentence = new List<WordBehaviour>();
        currentWordsThatCanBeSelected = new List<WordBehaviour>();
        sentenceContainerPanel.SetActive(true);
        wordSelectorPanel.SetActive(true);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        /* TODO:
           - Show the space of where "Words" can be dragged to
           - Show all the words the player has now
           -   */
        float yOffset = 0f;
        foreach (Word word in ActorManager.GetAllWordsPlayerHasCollected())
        {
            WordBehaviour currEmptyWord = Instantiate(emptyUIGameObject, wordSelectorRectangle).AddComponent<WordBehaviour>();
            currEmptyWord.SetWord(word);
            RectTransform currRect = currEmptyWord.GetComponent<RectTransform>();
            currRect.anchoredPosition = new Vector2(0, yOffset);
            yOffset += currRect.rect.height + 5f;
            currEmptyWord.GetComponent<TextMeshProUGUI>().text = currEmptyWord.word.presentedWord;
            
            currentWordsThatCanBeSelected.Add(currEmptyWord);
        }

    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentConversable = null;
        currentSentence.Clear();
        currentWordsThatCanBeSelected.Clear();
        
        sentenceContainerPanel.SetActive(false);
        wordSelectorPanel.SetActive(false);
        
        inputClickOnThings.action.performed -= OnInputActionClickOnThings;
        inputClickOnThings.action.Disable();
    }

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnInputActionClickOnThings(InputAction.CallbackContext context)
    {
        if (!IsWordClickedOn(out WordBehaviour foundWord)) return;
        
        Debug.Log("Found Word: " + foundWord.word.presentedWord);
        if (currentSentence.Contains(foundWord))
        {
            currentSentence.Remove(foundWord);
            Destroy(foundWord.gameObject);
        }
        else
            currentSentence.Add(Instantiate(foundWord, sentenceContainerRectangle));
        
        WordPositionsInSentence();
    }

    public void ExitState()
    {
        enabled = false;
    }

    public void EnterState()
    {
        enabled = true;
    }

    private bool IsWordClickedOn(out WordBehaviour foundWord)
    {
        foundWord = null;
        Vector2 mousePosition = Mouse.current.position.value;
        
        PointerEventData pointerEventData = new (EventSystem.current)
        {
            position = mousePosition
        };
        
        List<RaycastResult> results = new ();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult raycastResult in results)
        {
            if (!raycastResult.gameObject.TryGetComponent<WordBehaviour>(out WordBehaviour word)) continue;
            
            foundWord = word;
            return true;
        }

        return false;
    }

    private void WordPositionsInSentence()
    {
        /* TODO:
            Position all the words in the sentence.
            This Method is called after adding or removing words from the sentence.
            xoffset shouldnt be bigger than width of the panel - rectangle width halved
            if it is bigger then continue with 0 one rectangle height below.*/
        float xOffset = 20f;
        int index = 1;
        
        foreach (WordBehaviour wordBehaviour in currentSentence)
        {
            RectTransform rectangle = wordBehaviour.GetComponent<RectTransform>();
            
            rectangle.anchoredPosition = new Vector2(
            index * rectangle.rect.width * 0.5f + xOffset, 
            0);
            
            index++;
        }
    }

    public void SetIConversable(IConversable conversable)
    {
        currentConversable = conversable;
    }

    public void OnTimeRunOut()
    {
        
    }

    public void OnRespond()
    {
        Debug.Log("Button Clicked OnRespond");
        // currentSentence.ConvertAll(x => x.word).ForEach(x => Debug.Log(x.presentedWord));
        if (currentConversable.TryResponse(currentSentence.ConvertAll(x => x.word)))
        {
            currentConversable.StartSolutionChitChat();
            ActorControlTypeStateMachine.ChangeStateToListening(currentConversable);
        }
        else
            Debug.Log("Words are wrong");
    }
}
