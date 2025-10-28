using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sentence", menuName = "Scriptable Objects/Sentence")]
public class Sentence : ScriptableObject
{
    public List<WordData> words;
    public Tense tense;
    public Pronoun pronoun;
    public string finalSentence { get; private set; }

    public void SetSentence()
    {
        finalSentence = "";

        foreach (WordData wordData in words)
        {
            if (wordData is VerbData verbs)
            {
                finalSentence += " " + VerbConjugator.Conjugate(verbs, tense, pronoun);
            }
            else
            {
                finalSentence += " " + wordData.presentedWord;
            }
        }
    }
}
