using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructArea : ConstructBase
{
    [SerializeField]
    private string resourceName = "";
    [SerializeField]
    private float resourceRate = 0f;
    [SerializeField]
    private float radiusEffect = 0f;

    private GameObject player;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radiusEffect)
        {
            playerStats.ConsumeResource(resourceRate * Time.deltaTime, resourceName);
        }
    }


}
