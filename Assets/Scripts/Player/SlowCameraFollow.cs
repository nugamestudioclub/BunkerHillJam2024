using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCameraFollow : MonoBehaviour
{

    private Vector3 targetPosition;
    private CharacterController cc;
    [SerializeField]
    private float lookForward = 0.5f;
    [SerializeField]
    private float speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cc == null)
        {
            cc = PlayerController.Instance.GetComponent<CharacterController>();
            targetPosition = cc.transform.position;
        }

        targetPosition = new Vector3(cc.transform.position.x, cc.transform.position.y, transform.position.z) +
            Vector3.right * cc.velocity.x * lookForward;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*speed);
    }
}
