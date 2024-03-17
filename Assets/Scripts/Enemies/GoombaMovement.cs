using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool goingLeft = false;
    private Vector3 direction = Vector3.left;
    [SerializeField]
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingLeft)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }

    }
    
    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        goingLeft = !goingLeft;
    }
    private void OnCollisionEnter(Collision collision)
    {
        goingLeft = !goingLeft;
    }
}
