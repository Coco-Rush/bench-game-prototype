using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateListeningAction : MonoBehaviour, IControlTypeState
{
    [SerializeField] private InputActionReference inputToNextChitChat;
    [SerializeField] private InputActionReference inputSpeedUpText;
    private IConversable currentConversable;

    private void OnEnable()
    {
        inputToNextChitChat.action.Enable();
        inputSpeedUpText.action.Enable();

        inputSpeedUpText.action.started += OnInputActionStartedSpeedUpText;
        inputSpeedUpText.action.canceled += OnInputActionCanceledSpeedUpText;
        inputToNextChitChat.action.performed += OnInputActionPerformedToNextChitChat;
    }

    private void OnDisable()
    {
        inputSpeedUpText.action.started -= OnInputActionStartedSpeedUpText;
        inputSpeedUpText.action.canceled -= OnInputActionCanceledSpeedUpText;
        inputToNextChitChat.action.performed -= OnInputActionPerformedToNextChitChat;
        
        inputSpeedUpText.action.Disable();
        inputToNextChitChat.action.Disable();
    }

    private void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitState()
    {
        enabled = false;
    }

    public void EnterState()
    {
        enabled = true;
    }

    public void SetIConversable(IConversable conversable)
    {
        currentConversable = conversable;
    }

    private void OnInputActionPerformedToNextChitChat(InputAction.CallbackContext context)
    {
        if (currentConversable.NextChitChat())
        {
            // Means a next text is coming
        }
        else
        {
            // No more chit chat
            // Hide Speech Bubble?
            UIController.instance.HideSpeechBubble();
            ActorControlTypeStateMachine.Instance.ChangeStateToOverworldMovement();
        }
    }

    private void OnInputActionStartedSpeedUpText(InputAction.CallbackContext context)
    {
        
    }

    private void OnInputActionCanceledSpeedUpText(InputAction.CallbackContext context)
    {
        
    }
}
