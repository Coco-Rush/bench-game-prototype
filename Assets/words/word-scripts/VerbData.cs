using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "VerbData", menuName = "Scriptable Objects/VerbData")]
public class VerbData : WordData
{
    [SerializeField] private string infinitiveForm;
    [SerializeField] private string imperativeForm;
    [SerializeField] private string[] presentSimpleForm;
    [SerializeField] private string[] pastSimpleForm;
    [SerializeField] private string presentParticipleForm;
    [SerializeField] private string pastParticipleForm;

    public string infinitive => infinitiveForm;
    public string imperative => imperativeForm;
    public string[] present => presentSimpleForm;
    public string[] preterite => pastSimpleForm;
    public string presentParticiple => presentParticipleForm;
    public string pastParticiple => pastParticipleForm;
}
