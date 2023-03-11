using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    private float maxInteractDistance = 1f;

    private PlayerStats playerStats;

    public ConstructSelector cs;
    private bool selectorShown;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        //cs.gameObject.SetActive(false);
        selectorShown = false;

        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R Pressed");
            InteractWithObject();
        }

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisassembleObject();
        }
        */

        if (Input.GetKeyDown(KeyCode.E))
        {
            DisassembleObject();
            PlaceObject();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectorShown = !selectorShown;
        }
        if (selectorShown && !cs.gameObject.activeSelf)
        {
            cs.gameObject.SetActive(true);
        }
        if (!selectorShown && cs.gameObject.activeSelf)
        {
            cs.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void InteractWithObject()
    {
        RaycastHit hit;
        Debug.Log("interact");
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green, 3f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            Debug.Log(hit.collider.gameObject);
            //Debug.DrawRay(Camera.main.transform.position, hit.point, Color.green, 5f);
            if (hit.collider.gameObject.tag == "Construct")
            {
                ConstructMaker maker = hit.collider.gameObject.GetComponent<ConstructMaker>();
                if (maker != null)
                {
                    float change = maker.Interact();
                    playerStats.ConsumeResource(change, maker.GetResource());
                }
            }

            if (hit.collider.gameObject.tag == "Key")
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    public void DisassembleObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            if (hit.collider.gameObject.tag == "Construct")
            {
                int reward = hit.collider.gameObject.GetComponent<ConstructBase>().Disassemble();
                playerStats.ChangeNanites(reward);
            }
        }
    }

    public void PlaceObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            if (Vector3.Normalize(hit.normal) == Vector3.up && hit.collider.gameObject.tag != "Construct")
            {
                GameObject selectedConstruct = cs.GetSelected();
                int cost = selectedConstruct.GetComponent<ConstructBase>().GetCost();
                if (cost <= playerStats.GetNanites())
                {
                    GameObject construct = Instantiate(selectedConstruct, hit.point + Vector3.up * 0.3f, Quaternion.identity);
                    playerStats.ChangeNanites(-cost);
                }
            }
        }
    }
}