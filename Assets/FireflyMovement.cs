using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{

    private SpringJoint2D joint;
    private float maxDistance = 15.08f;
    private float minDistance = 2.58f;
    void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
    }

    
    void Update()
    {
        
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
