using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NpcBehaviour : MonoBehaviour, IInspectable,IConversable
{
    public UnityEvent TimeRunsOut { get; set; }
    
    [SerializeField] private List<Puzzle> puzzles;
    private Puzzle currentPuzzle;
    private int indexChitChat;
    private float currentTimeInSeconds;


    // delegate of function?
    private void Start()
    {
        TimeRunsOut = new UnityEvent();
    }

    private void Update()
    {
    }

    public void Inspect()
    {
        Debug.Log("You inspected " + gameObject.name);
    }

    

    public bool StartChitChat()
    {
        currentPuzzle = GetCurrentPuzzle(puzzles);
        indexChitChat = 0;

        if (currentPuzzle.IsUnityNull())
            return false;
        
        
        PlayChitChat(currentPuzzle.GetChitChat(indexChitChat));
        return true;
    }

    public bool NextChitChat()
    {
        if (indexChitChat + 1 >= currentPuzzle.GetChitChatCount())
        {
            PlayChitChat("");
            return false;
        }

        indexChitChat += 1;
        PlayChitChat(currentPuzzle.GetChitChat(indexChitChat));
        return true;
    }

    public bool StartTalkPrompt()
    {
        currentPuzzle = GetCurrentPuzzle(puzzles);

        if (currentPuzzle.IsUnityNull())
            return false;

        UIController.InsertPromptTextForTMP(currentPuzzle.GetPuzzlePrompt());
        currentTimeInSeconds = currentPuzzle.timeLimit;
        return true;
        
    }

    public bool TryResponse()
    {
        throw new System.NotImplementedException();
    }

    private Puzzle GetCurrentPuzzle(List<Puzzle> localPuzzles, int index = 0)
    {
        if (index >= localPuzzles.Count) return null;

        return localPuzzles[index].isSolved ? GetCurrentPuzzle(localPuzzles, index + 1) : localPuzzles[index];
    }

    private void PlayChitChat(string playThis)
    {
        // have string appear every character per frame?
        UIController.InsertTextForTMP(playThis);
        
    }
}
