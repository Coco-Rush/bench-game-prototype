using System;
using Unity.VisualScripting;
using UnityEngine;

public class ActorControlTypeStateMachine : MonoBehaviour
{
    [SerializeField] private StateOverworldMovement overworldMovement;
    [SerializeField] private StateListeningAction listeningAction;
    [SerializeField] private StateTalkingAction talkingAction;
    private static ActorControlTypeStateMachine instance;

    private IControlTypeState currentState;

    private void Awake()
    {
        if (!instance.IsUnityNull() && instance != this)
            Destroy(this);
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ChangeStateToOverworldMovement();
        listeningAction.enabled = false;
        talkingAction.enabled = false;
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
        Debug.Log("current State: " + currentState.GetType().ToString());
    }

    public static void ChangeStateToListening(IConversable currentConversable)
    {
        instance.ChangeState(instance.listeningAction);
        instance.listeningAction.SetIConversable(currentConversable);
    }

    public static void ChangeStateToOverworldMovement()
    {
        instance.ChangeState(instance.overworldMovement);
    }

    public static void ChangeStateToTalking(IConversable currentConversable)
    {
        instance.ChangeState(instance.talkingAction);
        instance.talkingAction.SetIConversable(currentConversable);
        Debug.Log(currentConversable);
        currentConversable.TimeRunsOut.AddListener(instance.talkingAction.OnTimeRunOut);
    }
}
