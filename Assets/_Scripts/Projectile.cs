using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    const int LOOKBACK_COUNT = 10;
    static List<Projectile> PROJECTILES = new List<Projectile>();

    [SerializeField]
    private bool _awake = true;

    public bool awake
    {
        get { return _awake; }
        private set { _awake = value; }
    }

    private Vector3 prevPos;
    private List<float> deltas = new List<float>();
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        awake = true;
        prevPos = new Vector3(1000, 1000, 0);
        deltas.Add(1000);
        PROJECTILES.Add(this);
        Debug.Log("Projectile added to list: " + gameObject.name);
    }

    void FixedUpdate()
    {
        if (rigid.isKinematic || !awake) return;

        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add(deltaV3.magnitude);
        prevPos = transform.position;

        while (deltas.Count > LOOKBACK_COUNT)
        {
            deltas.RemoveAt(0);
        }

        float maxDelta = 0;
        foreach (float f in deltas)
        {
            if (f > maxDelta) maxDelta = f;
        }

        if (maxDelta <= Physics.sleepThreshold)
        {
            Debug.Log("Projectile going to sleep: " + gameObject.name);
            awake = false;
            rigid.Sleep();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Projectile destroyed: " + gameObject.name);
        PROJECTILES.Remove(this);
    }

    static public void DESTROY_PROJECTILES()
    {
        Debug.Log("DESTROY_PROJECTILES called");
        PROJECTILES.RemoveAll(p => p == null); // Remove null references
        List<Projectile> projectilesCopy = new List<Projectile>(PROJECTILES);
        foreach (Projectile p in projectilesCopy)
        {
            if (p != null)
            {
                Debug.Log("Attempting to destroy projectile: " + p.gameObject.name);
                Destroy(p.gameObject);
            }
        }
    }
}