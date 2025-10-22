using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = System.Random;

public class StateTalkingAction : MonoBehaviour, IControlTypeState
{
    [SerializeField] private InputActionReference inputClickOnThings;
    [SerializeField] private InputActionReference inputUndoWordSelection;
    [SerializeField] private RectTransform sentenceContainer;
    private GameObject canvasSentenceBuilder => sentenceContainer.gameObject;
    private List<WordBehaviour> currentWordsInSentence = new ();

    private float currentTimeInSeconds;

    private IConversable currentConversable;

    private void OnEnable()
    {
        inputClickOnThings.action.Enable();
        inputUndoWordSelection.action.Enable();

        inputClickOnThings.action.performed += OnInputActionClickOnThings;
        inputUndoWordSelection.action.performed += OnInputActionUndoWordSelection;

    }

    private void OnDisable()
    {
        inputClickOnThings.action.performed -= OnInputActionClickOnThings;
        inputUndoWordSelection.action.performed -= OnInputActionUndoWordSelection;
        
        inputClickOnThings.action.Disable();
        inputUndoWordSelection.action.Disable();
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
        Debug.Log("WORD AHHH");
        if (!IsWordClickedOn(out WordBehaviour foundWord)) return;
        
        Debug.Log("Found Word: " + foundWord.word.presentedWord);
        
        
    }

    private void OnInputActionUndoWordSelection(InputAction.CallbackContext context)
    {
        if (!IsWordClickedOn(out WordBehaviour foundWord)) return;
        
        Debug.Log("A word has been found");
        
        if (!IsWordInSentence(foundWord)) return;
    }

    public void ExitState()
    {
        enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentConversable = null;
    }

    public void EnterState()
    {
        enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        /* TODO:
           - Show the space of where "Words" can be dragged to
           - Show all the words the player has now
           -   */
    }

    private bool IsWordClickedOn(out WordBehaviour foundWord)
    {
        foundWord = null;
        Vector2 mousePosition = Mouse.current.position.value;
        
        // ??? What is this Line?
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

    private void AddWordToSentence(WordBehaviour localWord)
    {
        /* TODO:
            Add Word to the end of the sentence.
            The same word can be added an infinite amount of times.
            */
    }

    private void RemoveWordFromSentence(WordBehaviour localWord)
    {
        /* TODO:
            Check if the position or rather the word selected is not just existent in the sentence, but actually a direct reference to the object in the list. (Because we instantiate words)
            If so then we can safely remove that word from that index position
            All words, which come after the removed word, will be moved one up in index
            */
    }

    private bool IsWordInSentence(WordBehaviour localWord)
    {
        return currentWordsInSentence.Contains(localWord);
    }

    public void SetIConversable(IConversable conversable)
    {
        currentConversable = conversable;
    }

    public void OnTimeRunOut()
    {
        
    }
}
