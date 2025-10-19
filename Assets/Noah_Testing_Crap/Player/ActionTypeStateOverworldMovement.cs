using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// In this script the following functionality is coded:
/// - Overworld Movement
/// - Detect Interactable, Inspectable and Conversable Objects
/// </summary>
public class ActionTypeStateOverworldMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference inputWalking;
    [SerializeField] private InputActionReference inputJumping;
    [SerializeField] private InputActionReference inputInteracting;
    [SerializeField] private InputActionReference inputInspecting;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float radiusOfSphereCastToCheckForInteractableThingsAroundPlayer;

    private UIController uiController;

    private CharacterController thisCharacterController;

    private ValueTuple<bool, IInspectable> inspectableInReach;
    private ValueTuple<bool, IInteractable> interactableInReach;
    private ValueTuple<bool, IConversable> conversableInReach;

    private void OnEnable()
    {
        inputWalking.action.Enable();
        inputJumping.action.Enable();
        inputInteracting.action.Enable();
        inputInspecting.action.Enable();

        inputJumping.action.performed += OnInputActionPerformedInputJumping;
        inputInteracting.action.performed += OnInputActionPerformedInputInteracting;
        inputInspecting.action.performed += OnInputActionPerformedInputInspecting;
    }

    private void OnDisable()
    {
        inputJumping.action.performed -= OnInputActionPerformedInputJumping;
        inputInteracting.action.performed -= OnInputActionPerformedInputInteracting;
        inputInspecting.action.performed -= OnInputActionPerformedInputInspecting;
        
        inputWalking.action.Disable();
        inputJumping.action.Disable();
        inputInteracting.action.Disable();
        inputInspecting.action.Disable();
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
        Vector3 movementVector = Vector3.right* inputDirectionVector.x + Vector3.forward*inputDirectionVector.y;
        thisCharacterController.Move(movementVector.normalized * (walkingSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.TryGetComponent<IInspectable>(out var inspectable))
        {
            // >>>> set a bool check to true and show UI element to communicate to player
            uiController.ShowUIElementInspect();
            inspectableInReach = (true,inspectable);
        }
        else
        {
            // Remove UI Element
            inspectableInReach.Item1 = false;
        }
            
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            // >>>> set a bool check to true and show UI element to communicate to player
            uiController.ShowUIElementInteract();
            interactableInReach = (true, interactable);
        }
        else
        {
            // Remove UI Element
            interactableInReach.Item1 = false;
        }
        if (other.TryGetComponent<IConversable>(out var conversable))
        {
            // >>>> set a bool check to true and show UI element to communicate to player
            uiController.ShowUIElementListen();
            uiController.ShowUIElementTalk();
            conversableInReach = (true, conversable);
        }
        else
        {
            // Remove UI Element
            conversableInReach.Item1 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.TryGetComponent<IInspectable>(out var inspectable))
        {
            uiController.HideUIElementInspect();
        }
    }

    private void OnInputActionPerformedInputJumping(InputAction.CallbackContext context)
    {
        
    }

    private void OnInputActionPerformedInputInteracting(InputAction.CallbackContext context)
    {
        if (interactableInReach.Item1)
        {
            // Enter Interact State
            // Action Depends on THIS interact object
            interactableInReach.Item2.Interact();
        }
    }

    private void OnInputActionPerformedInputInspecting(InputAction.CallbackContext context)
    {
        if (inspectableInReach.Item1)
        {
            // Inspect Object -> Gain new word in dictionary
            // Should
        }
    }

    private void OnInputActionPerformedInputListening(InputAction.CallbackContext context)
    {
        if (conversableInReach.Item1)
        {
            
        }
    }

    private void OnInputActionPerformedInputConverse(InputAction.CallbackContext context)
    {
        if (conversableInReach.Item1)
        {
            
        }
    }
}
