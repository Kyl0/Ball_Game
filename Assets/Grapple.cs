using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplepoint;
    private float maxDistance = 400f;
    private SpringJoint joint;
    public LayerMask grappleable;
    public Transform hookTip, cam, pl;



    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, cam.forward);
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
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

            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, hookTip.position);
        lr.SetPosition(1, grapplepoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
