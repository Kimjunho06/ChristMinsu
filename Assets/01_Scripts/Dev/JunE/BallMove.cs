using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;//BilliardsBall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("BilliardsBall"))
        {
            Debug.Log("123");
            rb.velocity = Vector3.zero;
        }
    }
}
