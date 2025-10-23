using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sentence", menuName = "Scriptable Objects/Sentence")]
public class Sentence : ScriptableObject
{
    public List<Word> words;
}
