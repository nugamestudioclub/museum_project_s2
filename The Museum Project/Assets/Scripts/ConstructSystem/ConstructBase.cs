using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBase : MonoBehaviour
{
    [SerializeField]
    private int naniteCost = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // called whenever the player collects the resources from this construct
    public void Interact()
    {
        
    }

    public int Disassemble()
    {
        Destroy(this.gameObject);
        return naniteCost;
    }

    public int GetCost()
    {
        return naniteCost;
    }
}