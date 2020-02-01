using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    public float speed = 5;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // talk to the rigid body
    }

    // Update is called once per frame
    void Update()
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
