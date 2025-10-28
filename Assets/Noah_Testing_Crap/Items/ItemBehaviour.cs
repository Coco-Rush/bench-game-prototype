using UnityEngine;

public class ItemBehaviour : MonoBehaviour, IInspectable
{
    [SerializeField] private ItemData itemData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Inspect()
    {
        // Player should learn this word
        if (ActorManager.AddWordToWordsCollected(itemData.learnThisWord))
        {
            Debug.Log("You have learned the following Word: " + itemData.learnThisWord.presentedWord);
            return;
        }
        
        Debug.Log("You already learned the following Word: " + itemData.learnThisWord.presentedWord);
    }
}
