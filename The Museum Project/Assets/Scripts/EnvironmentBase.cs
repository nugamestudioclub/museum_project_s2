using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoiseTest;

public class EnvironmentBase
{
    public OpenSimplexNoise test = new OpenSimplexNoise();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetTest(Vector2 pos)
    {
        decimal posx = new decimal(pos.x);
        decimal posy = new decimal(pos.y);
        return (float)test.Evaluate((double)posx, (double)posy);
    }

    public float GetNutrients()
    {
        return 1f;
    }

    public float GetTemperature()
    {
        return 1f;
    }
}
