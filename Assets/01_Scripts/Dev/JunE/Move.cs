using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float rot = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.left * 0.1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.right;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            rot += 0.01f;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.EulerRotation(0,rot,0);
    }
}
