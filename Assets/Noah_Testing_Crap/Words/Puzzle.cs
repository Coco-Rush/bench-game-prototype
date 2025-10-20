using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Scriptable Objects/Puzzle")]
public class Puzzle : ScriptableObject
{
    // Sentence
    [SerializeField] private List<Word> words;

    public List<string> chitChatListForEachPuzzle;
    public IReadOnlyList<Word> readOnlyWords => words;
    // timelimit
    [SerializeField] private float timelimitForPuzzleInSeconds;
    // isPuzzleSolved?
    [HideInInspector] public bool isSolved;
}
