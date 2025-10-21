using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour, IInspectable,IConversable
{
    [SerializeField] private List<Puzzle> puzzles;

    private Puzzle currentPuzzle;

    private int indexChitChat;


    // delegate of funciton?
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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
        
        
        PlayChitChat(currentPuzzle.chitChatListForEachPuzzle[indexChitChat]);
        return true;
    }

    public bool NextChitChat()
    {
        if (indexChitChat + 1 >= currentPuzzle.chitChatListForEachPuzzle.Count)
        {
            PlayChitChat("");
            return false;
        }

        indexChitChat += 1;
        PlayChitChat(currentPuzzle.chitChatListForEachPuzzle[indexChitChat]);
        return true;
    }

    public void StartTalkPrompt()
    {
        throw new System.NotImplementedException();
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
        UIController.instance.InsertTextForTMP(playThis);
        
    }
}
