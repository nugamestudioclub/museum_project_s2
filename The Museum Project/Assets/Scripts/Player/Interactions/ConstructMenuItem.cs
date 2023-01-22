using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructMenuItem
{
    private string name;
    private Sprite image;
    private string desc;
    private GameObject target;

    public ConstructMenuItem(string name, Sprite image, string desc)
    {
        this.name = name;
        this.image = image;
        this.desc = desc;
        target = null;
    }

    public ConstructMenuItem(string name, Sprite image, string desc, GameObject target)
    {
        this.name = name;
        this.image = image;
        int cost = target.GetComponent<ConstructBase>().GetCost();
        if (cost == 1)
        {
            this.desc = desc + "\nCosts " + cost + " Nanite.";
        }
        else
        {
            this.desc = desc + "\nCosts " + cost + " Nanites.";
        }
        this.target = target;
    }

    public string GetName()
    {
        return this.name;
    }

    public Sprite GetImage()
    {
        return this.image;
    }

    public string GetDesc()
    {
        return this.desc;
    }

    public GameObject GetTarget()
    {
        return this.target;
    }
}
