using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBand : MonoBehaviour
{
    [Header("Rubber Band Settings")]
    [SerializeField] private Transform LeftPoint;
    [SerializeField] private Transform LaunchPoint;
    [SerializeField] private Transform RightPoint;

    [Header("Prefab")]
    [SerializeField] private Transform projectilePrefab;

    [SerializeField] private LineRenderer slingshotString;

    // Start is called before the first frame update
    void Start()
    {
        slingshotString = GetComponent<LineRenderer>();
        slingshotString.SetPositions(new Vector3[3] {LeftPoint.position, LaunchPoint.position, RightPoint.position});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMidpoint(Vector3 position) {
        slingshotString.SetPosition(1, position);
    }
}