using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool goingLeft = false;
    private Vector3 direction = Vector3.left;
    public Vector3 Direction { get { return direction; } }
    [SerializeField]
    private float speed = 5f;
    [Tooltip("Max number of seconds without a turn at which point it forces a turn")]
    [SerializeField]
    private float maxTurnTimeInterval = 10f;
    private float lastTurnTime = -1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastTurnTime = Time.time;
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
        if(Time.time-lastTurnTime > maxTurnTimeInterval)
        {
            direction = -direction;
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
