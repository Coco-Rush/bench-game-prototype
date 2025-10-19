using UnityEngine;

public class NpcBehaviour : MonoBehaviour, IInspectable
{
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
        Debug.Log("You inspected " + gameObject.name);
    }
}
