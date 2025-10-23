using System;
using System.Collections.Generic;
using Mono.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static ActorManager instance;
    [SerializeField] private List<Word> wordsCollected;

    private void Awake()
    {
        if (!instance.IsUnityNull() && instance != this)
            Destroy(this);
        else
            instance = this;

        if (wordsCollected == null)
            wordsCollected = new List<Word>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public static IReadOnlyCollection<Word> GetAllWordsPlayerHasCollected()
    {
        return instance.wordsCollected;
    }
}
