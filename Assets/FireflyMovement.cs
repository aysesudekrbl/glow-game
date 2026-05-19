using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{

    //public ConnectionLine connectionLine;
    public RopeManager ropeManager;
    private SpringJoint2D joint;
    private float maxDistance = 15.08f;
    private float minDistance = 2.58f;
    void Awake()
    {
        joint = GetComponent<SpringJoint2D>();
    }

    
    void Update()
    {
        

        if (ropeManager.isRopeActive && Input.GetMouseButton(0))
    {
        if (joint.distance > minDistance)
            joint.distance -= 8f * Time.deltaTime;
    }
    else
    {
        if (ropeManager.isRopeActive && joint.distance < maxDistance)
            joint.distance += 8f * Time.deltaTime;
    }
        if(joint.distance > maxDistance)
        {
            joint.distance = maxDistance;
        }
    }
}
