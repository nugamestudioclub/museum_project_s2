using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private float health;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float healthDepleteRate = 1;

    private float hunger;
    [SerializeField]
    private float maxHunger = 20f;

    private float hydration;
    [SerializeField]
    private float maxHydration = 10f;

    [SerializeField]
    private int nanites = 10;

    private GameObject statUI;
    private Image healthContent;
    private Image hungerContent;
    private Image hydrationContent;
    private TMP_Text naniteContent;

    private Image thrustContent;

    [SerializeField]
    private Transform spawnpoint;

    private PlayerPhysics playerPhysics;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hunger = maxHunger;
        hydration = maxHydration;

        statUI = GameObject.Find("Survival Stats");
        healthContent = statUI.transform.Find("Health Bar/Health Value").gameObject.GetComponent<Image>();
        hungerContent = statUI.transform.Find("Hunger Bar/Hunger Value").gameObject.GetComponent<Image>();
        hydrationContent = statUI.transform.Find("Hydration Bar/Hydration Value").gameObject.GetComponent<Image>();
        naniteContent = statUI.transform.Find("Nanite Bar").gameObject.GetComponent<TMP_Text>();

        thrustContent = statUI.transform.Find("Thrust Bar/Thrust Value").gameObject.GetComponent<Image>();

        SetNanites(nanites);

        playerPhysics = gameObject.GetComponent<PlayerPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        TickStats();
    }

    //called each frame to update stat values
    public void TickStats()
    {
        TickHunger();
        TickHydration();
        if (hunger == 0f || hydration == 0f)
        {
            ChangeHealth(-healthDepleteRate * Time.deltaTime);
        }
        if (health == 0)
        {
            Debug.Log("player died");
            Respawn();
        }
    }

    public void ChangeStat(ref float stat, float maxStat, float change, ref Image statContent)
    {
        stat = Mathf.Clamp(stat + change, 0f, maxStat);
        statContent.fillAmount = Mathf.Clamp(stat / maxStat, 0f, 1f);
    }

    public void TickHunger()
    {
        ChangeHunger(-Time.deltaTime);
    }

    public void TickHydration()
    {
        ChangeHydration(-Time.deltaTime);
    }

    public void ChangeHealth(float change)
    {
        ChangeStat(ref health, maxHealth, change, ref healthContent);
    }

    public void ChangeHunger(float change)
    {
        ChangeStat(ref hunger, maxHunger, change, ref hungerContent);
    }

    public void ChangeHydration(float change)
    {
        ChangeStat(ref hydration, maxHydration, change, ref hydrationContent);
    }

    public void ConsumeResource(float change, string name)
    {
        if (name == "Food")
        {
            ChangeHunger(change);
        }
        else if (name == "Water")
        {
            ChangeHydration(change);
        }
        else if (name == "Health")
        {
            ChangeHealth(change);
        }
    }

    public int GetNanites()
    {
        return nanites;
    }

    public void SetNanites(int s)
    {
        nanites = s;
        naniteContent.SetText(nanites.ToString());
    }

    public void ChangeNanites(int c)
    {
        SetNanites(nanites + c);
    }

    public void Respawn()
    {
        // Note, this respawn script will have to be more advanced in later versions.
        ChangeHealth(maxHealth);
        ChangeHunger(maxHunger);
        ChangeHydration(maxHydration);
        transform.position = spawnpoint.position;
        transform.rotation = spawnpoint.rotation;
        playerPhysics.Reset();
    }

    public void SetThrust(float amount)
    {
        thrustContent.fillAmount = amount;
    }
}
