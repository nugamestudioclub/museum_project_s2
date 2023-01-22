using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DependAppear : MonoBehaviour
{
    public GameObject[] dependents;
    public GameObject[] targets;
    private bool allCollected;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var obj in targets)
        {
            obj.SetActive(false);
        }
        allCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!allCollected)
        {
            allCollected = dependents.Select(x => !x.activeSelf).Aggregate((x, y) => (x && y));
            if (allCollected)
            {
                foreach (var obj in targets)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
