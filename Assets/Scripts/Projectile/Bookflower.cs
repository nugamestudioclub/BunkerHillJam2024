using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookflower : MonoBehaviour
{
    private float direction;
    private bool isMoving { get { return direction != 0; } }
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speed = 5f;
    public void SetDirection(float direction)
    {
        this.direction = direction*speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction!=0)
        {
            rb.velocity = new Vector2(direction, rb.velocity.y);
        }
    }
}
