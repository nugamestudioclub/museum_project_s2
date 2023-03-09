using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [SerializeField]
    private float maxGrappleThrow;

    [SerializeField]
    private float grappleStrength;

    [SerializeField]
    public float grappledAirControl;

    [SerializeField]
    private GameObject grapplePointPrefab;
    private GameObject curPointObj;

    public bool grappled;
    private Vector3 grapplePos;

    [SerializeField]
    private LineRenderer lineRender;

    // Start is called before the first frame update
    void Start()
    {
        grappled = false;
        grapplePos = Vector3.zero;
        lineRender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShootGrapple();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ReleaseGrapple();
        }
        if (grappled)
        {
            lineRender.SetPosition(0, gameObject.transform.position);
            lineRender.SetPosition(1, grapplePos);
        }
    }

    public void ShootGrapple()
    {
        // perform a raycast from the player's reticle
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxGrappleThrow))
        {
            //Debug.DrawRay(Camera.main.transform.position, hit.point, Color.green, 5f);
            TraversalProperties tp = hit.collider.gameObject.GetComponent<TraversalProperties>();
            if (tp == null || tp.canGrappleOnto)
            {
                grappled = true;
                grapplePos = hit.point;
                if (curPointObj != null)
                {
                    Destroy(curPointObj);
                }
                curPointObj = Instantiate(grapplePointPrefab, hit.point, Quaternion.identity);
                lineRender.enabled = true;
            }
        }
    }

    public void ReleaseGrapple()
    {
        grappled = false;
        grapplePos = Vector3.zero;
        if (curPointObj != null)
        {
            Destroy(curPointObj);
        }
        lineRender.enabled = false;
    }

    public Vector3 PullForce(Vector3 playerPos)
    {
        if (!grappled)
        {
            return Vector3.zero;
        }
        else
        {
            return ((grapplePos - playerPos).normalized * grappleStrength);
        }
    }
}
