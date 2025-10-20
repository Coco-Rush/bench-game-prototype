using System;
using Unity.VisualScripting;
using UnityEngine;

public class ActorControlTypeStateMachine : MonoBehaviour
{
    [SerializeField] private StateOverworldMovement overworldMovement;
    [SerializeField] private StateListeningAction listeningAction;
    public static ActorControlTypeStateMachine Instance;

    private IControlTypeState currentState;

    private void Awake()
    {
        if (!Instance.IsUnityNull() && Instance != this)
            Destroy(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeStateToOverworldMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeState(IControlTypeState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void ChangeStateToListening(IConversable currentConversable)
    {
        ChangeState(listeningAction);
        listeningAction.SetIConversable(currentConversable);
    }

    public void ChangeStateToOverworldMovement()
    {
        ChangeState(overworldMovement);
    }
}
