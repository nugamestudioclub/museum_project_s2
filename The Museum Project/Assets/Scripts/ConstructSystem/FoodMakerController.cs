using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodMakerController : MonoBehaviour
{
    private float food;
    [SerializeField]
    private float foodMax = 50f;
    [SerializeField]
    private float foodRate = 1f;

    private TMP_Text foodIndicator;

    // Start is called before the first frame update
    void Start()
    {
        food = 0f;

        foodIndicator = transform.Find("FoodIndicator").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        food = Mathf.Clamp(food + foodRate * Time.deltaTime, 0f, foodMax);
        foodIndicator.SetText("Food:\n" + Mathf.FloorToInt(food));
    }

    // called whenever the player collects the resources from this construct
    public float CollectAll()
    {
        float amount = food;
        food = 0f;
        return amount;
    }

    public void Disassemble()
    {
        Destroy(this.gameObject);
    }
}