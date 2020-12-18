using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplepoint;
    private float maxDistance = 100f;
    private SpringJoint joint;
    public LayerMask grappleable;
    public Transform hookTip, cam, pl;



    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappleable))
        {
            grapplepoint = hit.point;
            joint = pl.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplepoint;

            float distFromPt = Vector3.Distance(pl.position, grapplepoint);

            joint.maxDistance = distFromPt * 0.8f;
            joint.minDistance = distFromPt * 0.25f;

            // may need to alter for best fit
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
        }
    }

    void StopGrapple()
    {

    }
}
