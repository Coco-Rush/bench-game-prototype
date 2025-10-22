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
    [SerializeField] private LayerMask wordLayerMask;
    private List<LanguageWord> currentWordsInSentence = new List<LanguageWord>();

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
        if (!IsWordClickedOn(out LanguageWord foundWord)) return;
    }

    private void OnInputActionUndoWordSelection(InputAction.CallbackContext context)
    {
        if (!IsWordClickedOn(out LanguageWord foundWord)) return;
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

    private bool IsWordClickedOn(out LanguageWord foundLanguageWord)
    {
        foundLanguageWord = null;
        Vector2 mousePosition = Mouse.current.position.value;
        
        // ??? What is this Line?
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult raycastResult in results)
        {
            if (!raycastResult.gameObject.TryGetComponent<WordBehaviour>(out var word)) continue;
            
            foundLanguageWord = word.languageWord;
            return true;
        }

        return false;
    }

    private void AddWordToSentence(LanguageWord localLanguageWord)
    {
        
    }

    private void RemoveWordFromSentence(LanguageWord localLanguageWord)
    {
        
    }

    public void SetIConversable(IConversable conversable)
    {
        currentConversable = conversable;
    }

    public void OnTimeRunOut()
    {
        
    }
}
