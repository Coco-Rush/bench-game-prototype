using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionTypeStateOverworldMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference inputWalking;
    [SerializeField] private InputActionReference inputJumping;
    [SerializeField] private InputActionReference inputInteracting;

    private CharacterController thisCharacterController;
    private void OnEnable()
    {
        inputWalking.action.Enable();
        inputJumping.action.Enable();
        inputInteracting.action.Enable();

        inputWalking.action.performed += OnInputActionPerformedInputMoving;
        inputJumping.action.performed += OnInputActionPerformedInputJumping;
        inputInteracting.action.performed += OnInputActionPerformedInputInteracting;
    }

    private void OnDisable()
    {
        inputJumping.action.performed -= OnInputActionPerformedInputJumping;
        inputInteracting.action.performed -= OnInputActionPerformedInputInteracting;
        inputWalking.action.performed -= OnInputActionPerformedInputMoving;
        
        inputWalking.action.Disable();
        inputJumping.action.Disable();
        inputInteracting.action.Disable();
    }

    private void Awake()
    {
        if (TryGetComponent<CharacterController>(out thisCharacterController))
            Debug.Log("Found Character Controller");
        else
        {
            throw new NullReferenceException();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 inputDirectionVector = inputWalking.action.ReadValue<Vector2>();
        Vector3 movementVector = Vector3.right* inputDirectionVector.x + Vector3.forward*inputDirectionVector.y;
        thisCharacterController.Move(movementVector.normalized * Time.deltaTime);
    }

    private void OnInputActionPerformedInputJumping(InputAction.CallbackContext context)
    {
        
    }

    private void OnInputActionPerformedInputInteracting(InputAction.CallbackContext context)
    {
        
    }

    private void OnInputActionPerformedInputMoving(InputAction.CallbackContext context)
    {
        Debug.Log("Input Movement Read");
    }
}
