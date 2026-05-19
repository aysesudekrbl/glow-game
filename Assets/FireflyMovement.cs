using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{

    public ConnectionLine connectionLine;
    private SpringJoint2D joint;
    private float maxDistance = 15.08f;
    private float minDistance = 2.58f;
    void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
    }

    
    void Update()
    {
        float mesafe = Vector3.Distance(connectionLine.firefly1.position, connectionLine.firefly2.position);
        connectionLine.sag = mesafe * 0.15f;

        if (Input.GetMouseButton(0))
    {
        if (joint.distance > minDistance)
            joint.distance -= 8f * Time.deltaTime;
    }
    else
    {
        if (joint.distance < maxDistance)
            joint.distance += 8f * Time.deltaTime;
    }
    }
}
