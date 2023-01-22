using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstructManager : MonoBehaviour
{
    [SerializeField]
    private bool isMaker = true;

    [SerializeField]
    private string resourceName = "";
    private float resource = 0f;
    [SerializeField]
    private float resourceMax = 50f;
    [SerializeField]
    private float resourceRate = 1f;

    private TMP_Text resourceIndicator;

    [SerializeField]
    private float radiusEffect = 5f;
    private GameObject player;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        resourceIndicator = transform.Find("ResourceIndicator").GetComponent<TMP_Text>();

        if (isMaker)
        {
            resourceIndicator = transform.Find("ResourceIndicator").GetComponent<TMP_Text>();
        }
        else
        {
            resourceIndicator.enabled = false;
            player = GameObject.FindGameObjectWithTag("Player");
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMaker)
        {
            if (resource != resourceMax)
            {
                resource = Mathf.Clamp(resource + resourceRate * Time.deltaTime, 0f, resourceMax);
                resourceIndicator.SetText(resourceName + ":\n" + Mathf.FloorToInt(resource));
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= radiusEffect)
            {
                playerStats.ChangeHealth(resourceRate * Time.deltaTime);
            }
        }
    }

    // called whenever the player collects the resources from this construct
    public float CollectAll()
    {
        if (isMaker)
        {
            float amount = resource;
            resource = 0f;
            return amount;
        }
        return 0f;
    }

    public void Disassemble()
    {
        Destroy(this.gameObject);
    }

    public bool IsMaker()
    {
        return isMaker;
    }

    public string GetResource()
    {
        return resourceName;
    }
}