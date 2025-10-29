using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SentenceData", menuName = "Scriptable Objects/SentenceData")]
public class SentenceData : ScriptableObject
{
    [HideInInspector] public int id;
    public List<WordData> words;
    public Tense tense;
    public Pronoun pronoun;
    public string finalSentence { get; private set; }

    public void SetSentence()
    {
        finalSentence = "";

        foreach (WordData wordData in words)
            if (wordData is VerbData verbs)
                finalSentence += " " + VerbConjugator.Conjugate(verbs, tense, pronoun);
            else
                finalSentence += " " + wordData.presentedWord;
    }
}
