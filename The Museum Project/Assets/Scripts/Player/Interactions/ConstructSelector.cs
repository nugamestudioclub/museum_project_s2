using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConstructSelector : MonoBehaviour
{
    public GameObject foodMakerPrefab;
    public GameObject waterMakerPrefab;
    public GameObject healthAreaPrefab;

    public TMP_Text constructName;
    public Image constructImage;
    public TMP_Text constructDesc;
    public GameObject selectedImage;

    private List<ConstructMenuItem> choices = new List<ConstructMenuItem>();

    private int currentIndex;
    private int selectedIndex;

    // Start is called before the first frame update
    void Start()
    {
        choices.Add(new ConstructMenuItem("Nutrient Consolidator", null, "Example desc for food maker.\n\n\n", foodMakerPrefab));
        choices.Add(new ConstructMenuItem("Water Condenser", null, "Condenses water from the atmosphere.\n\n\nRelies on local humidity.", waterMakerPrefab));
        choices.Add(new ConstructMenuItem("Regeneration Field", null, "Example desc for regen field.\n\n\n", healthAreaPrefab));

        currentIndex = 0;
        selectedIndex = 0;

        RenderPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = choices.Count-1;
            }
            RenderPanel();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;
            if (currentIndex >= choices.Count)
            {
                currentIndex = 0;
            }
            RenderPanel();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = currentIndex;
            RenderPanel();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }    
    }

    public void RenderPanel()
    {
        constructName.SetText(choices[currentIndex].GetName());
        constructImage.sprite = choices[currentIndex].GetImage();
        constructDesc.SetText(choices[currentIndex].GetDesc());
        if (selectedIndex == currentIndex)
        {
            selectedImage.SetActive(true);
        }
        else
        {
            selectedImage.SetActive(false);
        }
    }

    public GameObject GetSelected()
    {
        return choices[selectedIndex].GetTarget();
    }
}
