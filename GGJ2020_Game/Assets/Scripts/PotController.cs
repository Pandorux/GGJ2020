using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pot))]
public class PotController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody rb;

    protected Pot pot;
    protected GrabPoint rightGrabPoint;
    protected GrabPoint leftGrabPoint;
    public bool isOnGround
    {
        get
        {
            if(rigidBody.velocity.y > 0.25)
                return false;

            if(rigidBody.velocity.y < -0.25)
                return false;

            return true;
        }
    }

    public bool isClimbing;
    public bool canClimb
    {
        get
        {
            if(rightGrabPoint?.canGrab)
                return true;

            if(leftGrabPoint?.canGrab)
                return false;

            return false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // talk to the rigid body
        pot = GetComponent<Pot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnGround)
        {
            GroundMovement();
        }
        else if(canClimb)
        {
            if(Input.GetKeyDown(Keycode.Mouse0) && rightGrabPoint.canGrab)
            {
                rightGrabPoint.isGrabbing = !rightGrabPoint.isGrabbing;

                // Shenanigans with left grab point
            }

            if(Input.GetKeyDown(Keycode.Mouse1) && leftGrabPoint.canGrab)
            {
                leftGrabPoint.isGrabbing = !leftGrabPoint.isGrabbing;

                // Shenanigans with left grab point
            }

            isClimbing = rightGrabPoint.isGrabbing || leftGrabPoint.isGrabbing ?
                true : false;
        }
    }

    protected void GroundMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // move n shit
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    //void OnTriggerEnter(Collider other) // when you hit the things, they deactivate
    //{
    //    if (other.gameObject.CompareTag("Pick Up"))
    //    {
    //        other.gameObject.SetActive(false);
    //    }
    //}
}
