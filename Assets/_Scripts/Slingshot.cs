using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;
    public GameObject projLinePrefab;
    public SlingshotLine slingshotLine; // Reference to the SlingshotLine script

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

        // Get reference to SlingshotLine script
        slingshotLine = GetComponent<SlingshotLine>();
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;

        // Instantiate projectile and attach it to the Line Renderer
        projectile = Instantiate(projectilePrefab);
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;

        //  Tell SlingshotLine to follow this projectile
        if (slingshotLine != null)
        {
            slingshotLine.SetProjectile(projectile.transform);
        }
    }

    void Update()
    {
        if (!aimingMode) return;

        // Convert mouse position to world space
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move projectile to the adjusted position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        //  Update Line Renderer dynamically
        if (slingshotLine != null)
        {
            slingshotLine.SetProjectile(projectile.transform);
        }

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta * velocityMult;

            //  Remove the projectile reference from the Line Renderer (hides the band)
            if (slingshotLine != null)
            {
                slingshotLine.ReleaseProjectile();
            }

            // Switch to slingshot view immediately before setting POI
            FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);
            FollowCam.POI = projectile;

            // Add projectile line trail
            Instantiate<GameObject>(projLinePrefab, projectile.transform);

            // Nullify the projectile reference
            projectile = null;

            // Notify MissionDemolition
            MissionDemolition.SHOT_FIRED();
        }
    }
}

