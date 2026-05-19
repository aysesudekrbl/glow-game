using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ConnectionLine : MonoBehaviour
{
    private LineRenderer line;
    public Transform firefly1;
    public Transform firefly2;
    
    public float sag = 1.5f;
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        line.positionCount = 20;
        line.SetPosition(0,firefly1.position);
        line.SetPosition(19,firefly2.position);
        
    }
    
    void Update()
    {
        line.SetPosition(0,firefly1.position);
        line.SetPosition(19,firefly2.position);
        float distance = firefly2.position.x - firefly1.position.x;
        float dBetween = distance / 19;

        for(int i = 1; i < 19; i++)
        {
            float t = i / 19f;
            float sarkma = Mathf.Sin(t * Mathf.PI) * sag;
            line.SetPosition(i, new Vector3(firefly1.position.x + (dBetween * i), firefly1.position.y - sarkma, 0));
            
        }
    }
}

