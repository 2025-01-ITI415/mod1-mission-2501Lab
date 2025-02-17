using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotString : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Slingshot slingshot;
    private Transform leftAnchor, rightAnchor;

    void Awake()
    {
        slingshot = GetComponent<Slingshot>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Set color using a default material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;  // Change this to any color
        lineRenderer.endColor = Color.black;

        // Assume Slingshot has two anchor points as children
        leftAnchor = transform.Find("LeftAnchor");
        rightAnchor = transform.Find("RightAnchor");
    }

    void Update()
    {
        if (slingshot.aimingMode && slingshot.projectile != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, leftAnchor.position);
            lineRenderer.SetPosition(1, slingshot.projectile.transform.position);
            lineRenderer.SetPosition(2, rightAnchor.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
