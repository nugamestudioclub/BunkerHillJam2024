using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCameraFollow : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve velCurve;

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
    void LateUpdate()
    {
        if(cc == null)
        {
            cc = PlayerController.Instance.GetComponent<CharacterController>();
            targetPosition = cc.transform.position;
        }

        Debug.Log("vel: " + cc.velocity.x);
        targetPosition = new Vector3(cc.transform.position.x, cc.transform.position.y, transform.position.z) +
            Vector3.right * cc.velocity.x * lookForward * velCurve.Evaluate(Mathf.Abs(cc.velocity.x / 25));
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*speed);
    }
}
