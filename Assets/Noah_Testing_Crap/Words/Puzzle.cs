using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Scriptable Objects/Puzzle")]
public class Puzzle : ScriptableObject
{
    // Sentence
    [SerializeField] private List<Word> correctWords;
    [SerializeField] private List<string> chitChatListForEachPuzzle;
    
    [SerializeField] private float timelimitForPuzzleInSeconds;
    public float timeLimit => timelimitForPuzzleInSeconds;

    // isPuzzleSolved?
    [HideInInspector] public bool isSolved;
    [SerializeField] private List<string> showThisWhenPuzzleSolved;
    [SerializeField] private string showThisWhenResponseOfPlayerIsFalse;
    [SerializeField] private string showThisWhenTimeOfPuzzleRunsOut;
    [SerializeField] private string showThisAsPuzzlePrompt;
    
    public IReadOnlyList<Word> GetSolutionSentence()
    {
        return correctWords;
    }

    public IReadOnlyList<string> GetTextForWhenPuzzleIsSolved()
    {
        return showThisWhenPuzzleSolved;
    }

    public string GetChitChat(int index)
    {
        return chitChatListForEachPuzzle[index];
    }

    public int GetChitChatCount()
    {
        return chitChatListForEachPuzzle.Count;
    }

    public string GetPuzzlePrompt()
    {
        return showThisAsPuzzlePrompt;
    }
}
