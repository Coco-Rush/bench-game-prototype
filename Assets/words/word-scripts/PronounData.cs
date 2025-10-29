using UnityEngine;

[CreateAssetMenu(fileName = "PronounData", menuName = "Scriptable Objects/PronounData")]
public class PronounData : WordData
{
    [SerializeField] private Pronoun pronoun;
    public Pronoun thisPronoun => pronoun;
}
