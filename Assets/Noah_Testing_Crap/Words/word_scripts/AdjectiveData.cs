using UnityEngine;

[CreateAssetMenu(fileName = "AdjectiveData", menuName = "Scriptable Objects/AdjectiveData")]
public class AdjectiveData : WordData
{
    [SerializeField] private string formBase;
    [SerializeField] private string formMore;
    [SerializeField] private string formMost;
    [SerializeField] private bool hasOnlyBaseForm;
}
