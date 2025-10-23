using UnityEngine;

public class WordBehaviour : MonoBehaviour
{
    public Word word;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWord(Word localWord)
    {
        if (!word)
            word = localWord;
    }
}
