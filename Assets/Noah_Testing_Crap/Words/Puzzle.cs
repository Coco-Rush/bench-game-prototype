using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Scriptable Objects/Puzzle")]
public class Puzzle : ScriptableObject
{
    // Sentence
    [SerializeField] private List<Word> words;

    public IReadOnlyList<Word> readOnlyWords => words;
    // timelimit
    [SerializeField] private float timelimitForPuzzleInSeconds;
    // isPuzzleSolved?
    private bool isSolved;
}
