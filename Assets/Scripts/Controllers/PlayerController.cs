using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float playerSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.up* playerSpeed);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector3.up* playerSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * playerSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * playerSpeed);
        }
    }
}
