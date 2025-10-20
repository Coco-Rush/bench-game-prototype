using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }

    [SerializeField] private GameObject highlightInspect;
    [SerializeField] private GameObject highlightInteract;
    [SerializeField] private GameObject highlightListen;
    [SerializeField] private GameObject highlightTalk;
    [SerializeField] private TextMeshProUGUI speechTMPForSpeechInBubble;

    private GameObject instantiatedHighlightInspect;
    private GameObject instantiatedHighlightInteract;
    private GameObject instantiatedHighlightTalk;
    private GameObject instantiatedHighlightListen;

    private void Awake()
    {
        if (!instance.IsUnityNull() && instance != this)
            Destroy(this);
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ShowUIElementInspect()
    {
        instantiatedHighlightInspect = Instantiate(highlightInspect, transform);
    }

    public void ShowUIElementInteract()
    {
        instantiatedHighlightInteract = Instantiate(highlightInteract, transform);
    }

    public void ShowUIElementListen()
    {
        instantiatedHighlightListen = Instantiate(highlightListen, transform);
    }

    public void ShowUIElementTalk()
    {
        instantiatedHighlightTalk = Instantiate(highlightTalk, transform);
    }

    private void ShowUIElement()
    {
        
    }

    public void HideUIElementInspect()
    {
        Destroy(instantiatedHighlightInspect);
    }

    public void HideUIElementInteract()
    {
        Destroy(instantiatedHighlightInteract);
    }

    public void HideUIElementListen()
    {
        Destroy(instantiatedHighlightListen);
    }

    public void HideUIElementTalk()
    {
        Destroy(instantiatedHighlightTalk);
    }

    public void ShowSpeechBubble()
    {
        
    }

    public void HideSpeechBubble()
    {
        
    }

    public void InsertTextForTMP(string followingText)
    {
        speechTMPForSpeechInBubble.text = followingText;
    }
}
