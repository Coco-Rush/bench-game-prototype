using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Scriptable Objects/Puzzle")]
public class Puzzle : ScriptableObject
{
    // Sentence
    [SerializeField] private Sentence correctSentence;
    [SerializeField] private List<string> dialogChitChat;
    
    [SerializeField] private float timelimitForPuzzleInSeconds;
    public float timeLimit => timelimitForPuzzleInSeconds;

    // isPuzzleSolved?
    public bool isSolved;
    [SerializeField] private List<string> dialogPuzzleSolved;
    [SerializeField] private string dialogResponseFalse;
    [SerializeField] private string dialogTimeRunOut;
    [SerializeField] private string dialogPuzzlePrompt;
    
    public IReadOnlyList<WordData> GetSolutionWords()
    {
        List<WordData> correctWords = new ();
        foreach (WordData sentenceWord in correctSentence.words)
        {
            correctWords.Add(sentenceWord);
        }
        return correctWords;
    }

    public Sentence GetSolutionSentence()
    {
        return correctSentence;
    }

    public ReadOnlyCollection<string> GetTextForWhenPuzzleIsSolved()
    {
        return dialogPuzzleSolved.AsReadOnly();
    }

    public int GetCountDialogPuzzleSolved()
    {
        return dialogPuzzleSolved.Count;
    }

    public string GetChitChat(int index)
    {
        return dialogChitChat[index];
    }

    public int GetChitChatCount()
    {
        return dialogChitChat.Count;
    }

    public string GetPuzzlePrompt()
    {
        return dialogPuzzlePrompt;
    }
}
