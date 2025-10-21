using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateListeningAction : MonoBehaviour, IControlTypeState
{
    [SerializeField] private InputActionReference inputToNextChitChat;
    [SerializeField] private InputActionReference inputSpeedUpText;
    private IConversable currentConversable;
    private bool hasPressedInputToNextChitChat;

    private void OnEnable()
    {
        inputToNextChitChat.action.Enable();
        inputSpeedUpText.action.Enable();

        inputSpeedUpText.action.started += OnInputActionStartedSpeedUpText;
        inputSpeedUpText.action.canceled += OnInputActionCanceledSpeedUpText;
        inputToNextChitChat.action.started += OnInputActionStartedToNextChitChat;
    }

    private void OnDisable()
    {
        inputSpeedUpText.action.started -= OnInputActionStartedSpeedUpText;
        inputSpeedUpText.action.canceled -= OnInputActionCanceledSpeedUpText;
        inputToNextChitChat.action.started -= OnInputActionStartedToNextChitChat;
        
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
        //Debug.Log(inputToNextChitChat.action.ReadValue<float>());
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

    private void OnInputActionStartedToNextChitChat(InputAction.CallbackContext context)
    {
        Debug.Log(context.action + " started ChitChat/Does Next Chit Chat");
        if (currentConversable.NextChitChat()) return;
        // No more chit chat
        // Hide Speech Bubble?
        UIController.instance.HideSpeechBubble();
        ActorControlTypeStateMachine.ChangeStateToOverworldMovement();
    }

    private void OnInputActionStartedSpeedUpText(InputAction.CallbackContext context)
    {
        
    }

    private void OnInputActionCanceledSpeedUpText(InputAction.CallbackContext context)
    {
        
    }
}
