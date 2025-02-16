using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform leftAnchor, rightAnchor;
    private Transform projectile;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Line Renderer settings
        lineRenderer.positionCount = 3;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (projectile != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, leftAnchor.position);
            lineRenderer.SetPosition(1, projectile.position);
            lineRenderer.SetPosition(2, rightAnchor.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void SetProjectile(Transform newProjectile)
    {
        projectile = newProjectile;
    }

    public void ReleaseProjectile()
    {
        projectile = null;
    }
}

