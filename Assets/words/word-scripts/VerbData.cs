using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "VerbData", menuName = "Scriptable Objects/VerbData")]
public class VerbData : WordData
{
    public string infinitive;
    public string imperative;
    public string[] present = new string[6];
    public string[] preterite = new string[6];
    public string presentParticiple;
    public string pastParticiple;

}
