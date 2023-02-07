using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float defaultSpeed = 5;
    [SerializeField] private float dashSpeed = 2;   
    [SerializeField] private float jumpPower = 5;

    private int maxJumpCount = 2;
    private int jumpCount;

    private float speed;

    Rigidbody rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = defaultSpeed;
        jumpCount = 0;
    }

    private void Update()
    {
        PlayerInput();
   
    }

    private void PlayerInput()
    {
        PlayerMove(KeyCode.W, Vector3.forward);
        PlayerMove(KeyCode.S, Vector3.back);
        PlayerMove(KeyCode.A, Vector3.left);
        PlayerMove(KeyCode.D, Vector3.right);

        if (jumpCount < maxJumpCount)
        {
            PlayerJump(KeyCode.LeftControl);
            PlayerJump(KeyCode.RightControl);
        }
    }

    private void PlayerMove(KeyCode key, Vector3 dir)
    {
        if (Input.GetKey(key))
        {
            rigid.velocity = new Vector3(dir.x * speed, rigid.velocity.y, dir.z * speed);
            //transform.position += dir.normalized * Time.deltaTime * speed;
        }

        speed = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) ? defaultSpeed + dashSpeed : defaultSpeed;
    }

    private void PlayerJump(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount++;
        }
        if (Input.GetKeyUp(key))
        {
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y / 2, rigid.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
