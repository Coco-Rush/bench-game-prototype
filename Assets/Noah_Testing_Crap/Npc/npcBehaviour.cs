using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NpcBehaviour : MonoBehaviour, IInspectable,IConversable
{
    [SerializeField] private List<Puzzle> puzzles;
    private Puzzle currentPuzzle;
    private int indexChitChat;
    private float currentTimeInSeconds;


    // delegate of function?
    private void Start()
    {
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
        {
            currentPuzzle = puzzles[^1];
            return false;
        }
        
        
        PlayChitChat(currentPuzzle.GetChitChat(indexChitChat));
        return true;
    }

    public bool NextChitChat()
    {
        if (currentPuzzle.isSolved)
        {
            // Should show the SoluitonDialogo
            if (indexChitChat + 1 >= currentPuzzle.GetCountDialogPuzzleSolved())
            {
                PlayChitChat("");
                return false;
            }

            indexChitChat += 1;
            PlayChitChat(currentPuzzle.GetTextForWhenPuzzleIsSolved()[indexChitChat]);
        }
        else
        {
            if (indexChitChat + 1 >= currentPuzzle.GetChitChatCount())
            {
                PlayChitChat("");
                return false;
            }

            indexChitChat += 1;
            PlayChitChat(currentPuzzle.GetChitChat(indexChitChat));
        }

        return true;

    }

    public bool StartTalkPrompt()
    {

        currentPuzzle = GetCurrentPuzzle(puzzles);
        indexChitChat = 0;

        if (currentPuzzle.IsUnityNull())
        {
            currentPuzzle = puzzles[^1];
            return false;
        }
        
        UIController.InsertPromptTextForTMP(currentPuzzle.GetPuzzlePrompt());
        currentTimeInSeconds = currentPuzzle.timeLimit;
        return true;
        
    }

    public bool TryResponse(List<Word> tryWords)
    {
        Debug.Log("Enter TryResponse(List<Word>) of: " + gameObject.name);
        if (tryWords.Count != currentPuzzle.GetSolutionWords().Count) return false;

        for (int i = 0; i < tryWords.Count; i++)
        {
            if (tryWords[i].Equals(currentPuzzle.GetSolutionWords()[i])) continue;
            Debug.Log("Words are false");
            return false;
        }

        currentPuzzle.isSolved = true;
        Debug.Log("Words are correct");
        return true;
    }

    public void StartSolutionChitChat()
    {
        indexChitChat = 0;
        PlayChitChat(currentPuzzle.GetTextForWhenPuzzleIsSolved()[indexChitChat]);
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
