using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstructMaker : ConstructBase
{
    [SerializeField]
    private string resourceName = "";
    private float resource = 0f;
    [SerializeField]
    private float resourceMax = 0f;
    [SerializeField]
    private float resourceRate = 0f;

    private TMP_Text resourceIndicator;

    void Start()
    {
        resourceIndicator = transform.Find("ResourceIndicator").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (resource != resourceMax)
        {
            resource = Mathf.Clamp(resource + resourceRate * Time.deltaTime, 0f, resourceMax);
            resourceIndicator.SetText(resourceName + ":\n" + Mathf.FloorToInt(resource));
        }
    }

    public void SetProperties(string name, float max, float rate)
    {
        resourceName = name;
        resourceMax = max;
        resourceRate = rate;
    }

    public new float Interact()
    {
        float amount = resource;
        resource = 0f;
        return amount;
    }

    public string GetResource()
    {
        return resourceName;
    }
}
