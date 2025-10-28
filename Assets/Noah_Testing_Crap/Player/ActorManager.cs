using System;
using System.Collections.Generic;
using Mono.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static ActorManager instance;
    [SerializeField] private List<WordData> wordsCollected;
    [SerializeField] private VerbData toHave;
    [SerializeField] private VerbData toBe;
    [SerializeField] private VerbData will;

    private void Awake()
    {
        if (!instance.IsUnityNull() && instance != this)
            Destroy(this);
        else
            instance = this;

        if (wordsCollected == null)
            wordsCollected = new List<WordData>();
            
        VerbConjugator.SetAuxiliaryVerbs(toHave,toBe,will);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public static IReadOnlyCollection<WordData> GetAllWordsPlayerHasCollected()
    {
        return instance.wordsCollected;
    }

    public static bool AddWordToWordsCollected(WordData localWord)
    {
        if (instance.wordsCollected.Contains(localWord)) return false;
        
        instance.wordsCollected.Add(localWord);
        return true;
    }
}
