using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Projectile>() != null)
        {
            print("HIT!");
            Collider[] colliders = this.GetComponents<Collider>();
            foreach(Collider collider in colliders)
            {
                collider.enabled = false;
            }
            this.GetComponent<GoombaMovement>().enabled = false;
        }
    }
}
