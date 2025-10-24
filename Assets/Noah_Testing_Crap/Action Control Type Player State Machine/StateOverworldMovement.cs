using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// In this script the following functionality is coded:
/// - Overworld Movement
/// - Detect Interactable, Inspectable and Conversable Objects
/// </summary>
public class StateOverworldMovement : MonoBehaviour, IControlTypeState
{
    [SerializeField] private InputActionReference inputWalking;
    [SerializeField] private InputActionReference inputJumping;
    [SerializeField] private InputActionReference inputInteracting;
    [SerializeField] private InputActionReference inputInspecting;
    [SerializeField] private InputActionReference inputListening;
    [SerializeField] private InputActionReference inputTalking;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float radiusOfSphereCastToCheckForInteractableThingsAroundPlayer;

    private UIController uiController;

    private CharacterController thisCharacterController;

    private IInteractable currentInteractable;
    private IInspectable currentInspectable;
    private IConversable currentConversable;

    private void OnEnable()
    {
        inputWalking.action.Enable();
        inputJumping.action.Enable();
        inputInteracting.action.Enable();
        inputInspecting.action.Enable();
        inputListening.action.Enable();
        inputTalking.action.Enable();

        inputJumping.action.performed += OnInputActionPerformedInputJumping;
        inputInteracting.action.performed += OnInputActionPerformedInputInteracting;
        inputInspecting.action.performed += OnInputActionPerformedInputInspecting;
        inputListening.action.performed += OnInputActionPerformedInputListening;
        inputTalking.action.performed += OnInputActionPerformedInputTalking;
        
         // TODO: Get all UI elements that have been deactivated and re-activate them.
    }

    private void OnDisable()
    {
        inputJumping.action.performed -= OnInputActionPerformedInputJumping;
        inputInteracting.action.performed -= OnInputActionPerformedInputInteracting;
        inputInspecting.action.performed -= OnInputActionPerformedInputInspecting;
        inputListening.action.performed -= OnInputActionPerformedInputListening;
        inputTalking.action.performed -= OnInputActionPerformedInputTalking;

        inputWalking.action.Disable();
        inputJumping.action.Disable();
        inputInteracting.action.Disable();
        inputInspecting.action.Disable();
        inputListening.action.Disable();
        inputTalking.action.Disable();
        
        // TODO: Get all active UI Elements and deactivate them.
    }

    private void Awake()
    {
        if (!TryGetComponent<CharacterController>(out thisCharacterController))
            throw new NullReferenceException();
    }

    private void Start()
    {
        uiController = UIController.instance;
    }

    private void Update()
    {
        Vector2 inputDirectionVector = inputWalking.action.ReadValue<Vector2>();
        Vector3 movementVector = Vector3.right * inputDirectionVector.x + Vector3.forward * inputDirectionVector.y;
        thisCharacterController.Move(movementVector.normalized * (walkingSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.TryGetComponent<IInspectable>(out var inspectable))
        {
            // >>>> set a bool check to true and show UI element to communicate to player
            uiController.ShowUIElementInspect();
            currentInspectable = inspectable;
        }

        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            // >>>> set a bool check to true and show UI element to communicate to player
            uiController.ShowUIElementInteract();
            currentInteractable = interactable;
        }

        if (other.TryGetComponent<IConversable>(out var conversable))
        {
            // >>>> set a bool check to true and show UI element to communicate to player
            uiController.ShowUIElementListen();
            uiController.ShowUIElementTalk();
            currentConversable = conversable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInspectable>(out _))
        {
            uiController.HideUIElementInspect();
            currentInspectable = null;
        }

        if (other.TryGetComponent<IInteractable>(out _))
        {
            uiController.HideUIElementInteract();
            currentInteractable = null;
        }

        if (other.TryGetComponent<IConversable>(out _))
        {
            uiController.HideUIElementListen();
            uiController.HideUIElementTalk();
            currentConversable = null;
        }
    }

    private void OnInputActionPerformedInputJumping(InputAction.CallbackContext context)
    {
    }

    private void OnInputActionPerformedInputInteracting(InputAction.CallbackContext context)
    {
        if (currentInteractable.IsUnityNull()) return;
        // Enter Interact State
        // Action Depends on THIS interact object
    }

    private void OnInputActionPerformedInputInspecting(InputAction.CallbackContext context)
    {
        if (currentInspectable.IsUnityNull()) return;
        // Inspect Object -> Gain new word in dictionary
        // Should
    }

    private void OnInputActionPerformedInputListening(InputAction.CallbackContext context)
    {
        if (currentConversable.IsUnityNull()) return;

        if (!currentConversable.StartChitChat())
            currentConversable.StartSolutionChitChat(); 

        // TODO Delegate a function that shows all the correct UI Elements
        uiController.ShowSpeechBubble();
        uiController.HideUIElementInspect();
        uiController.HideUIElementInteract();
        uiController.HideUIElementTalk();
        uiController.HideUIElementListen();

        ActorControlTypeStateMachine.ChangeStateToListening(currentConversable);
    }

    private void OnInputActionPerformedInputTalking(InputAction.CallbackContext context)
    {
        if (currentConversable.IsUnityNull()) return;

        if (!currentConversable.StartTalkPrompt()) return;
        
        uiController.ShowSpeechBubble();
        uiController.HideUIElementInspect();
        uiController.HideUIElementInteract();
        uiController.HideUIElementTalk();
        uiController.HideUIElementListen();

        ActorControlTypeStateMachine.ChangeStateToTalking(currentConversable);
    }

    public void ExitState()
    {
        enabled = false;
    }

    public void EnterState()
    {
        enabled = true;
    }
}
