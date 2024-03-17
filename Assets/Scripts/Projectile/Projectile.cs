using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float direction;
    private bool isMoving { get { return direction != 0; } }
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lifetime = 2f;
    private float initTime;
    public void SetDirection(float direction)
    {
        this.direction = direction*speed;
        rb.AddForce(Vector3.right * this.direction, ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction!=0)
        {
            //rb.velocity = new Vector2(direction, rb.velocity.y);
        }
        if(Time.time-initTime > lifetime)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GoombaComponent>() != null)
        {
            //Destroy(this.gameObject);
        }
    }
    
}
