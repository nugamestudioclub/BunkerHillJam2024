using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController controller;
    public static PlayerController Instance { get { return controller; } }
    private CharacterController player;

    [SerializeField]
    private static float GRAVITY = 0.2f;
    [SerializeField]
    private static float NORMAL_JUMP_HEIGHT = 0.04f;
    [SerializeField]
    private static float NORMAL_MOVE_SPEED = 0.2f;
    [SerializeField]
    private static float FRICTION = 10f;
    [SerializeField]
    private GameObject firePrefab;
    private float lastDir = 1;

    [SerializeField]
    private LayerMask l;

    private Vector2 vel = Vector2.zero;

    private bool groundedPrevFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateController();

        UpdateDirection();
    }

    private void UpdateController()
    {
        if (!player.isGrounded && groundedPrevFrame)
        {
            groundedPrevFrame = false;
        }

        LateralMovement();
        vel.x = Mathf.Lerp(vel.x, 0, Time.deltaTime * FRICTION);

        vel.y -= GRAVITY * Time.deltaTime;

        if (player.isGrounded)
        {
            groundedPrevFrame = true;
            //player.Move(new Vector3(0, -2, 0));
            vel.y = 0;

            // testing purposes only
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Jump();
            }
        }

        player.Move(vel);

        //PlayerController.IsGrounded();
        //Debug.Log(controller.player.transform.position);
        //Debug.Log(PlayerController.IsGrounded());

    }

    private void UpdateDirection()
    {
        CharacterController cc = GetComponent<CharacterController>();
        if (cc.velocity.x != 0)
        {
            lastDir = cc.velocity.magnitude * cc.velocity.x;
        }
    }
    
    private void LateralMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x += NORMAL_MOVE_SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x -= NORMAL_MOVE_SPEED * Time.deltaTime;
        }
    }

    public static bool IsGrounded()
    {
        controller.player.TryGetComponent<CharacterController>(out var c);
        //Debug.Log(Physics.Raycast(controller.player.transform.position, Vector2.down).collider);
        return Physics.Raycast(controller.player.transform.position, Vector2.down, c.height / 2 + 0.1f, controller.l);
    }

    public static void Jump()
    {
        Debug.Log("jumping now!");
        controller.vel.y = NORMAL_JUMP_HEIGHT;
        controller.player.Move(Vector3.up * controller.vel.y);
    }

    private void _Shoot()
    {
        GameObject proj = Instantiate(firePrefab);
        proj.transform.parent = null;
        
        proj.transform.position = this.transform.position + (Vector3.right * lastDir / Mathf.Abs(lastDir));
        proj.GetComponent<Projectile>().SetDirection(lastDir/Mathf.Abs(lastDir));
    }
    public static void Shoot()
    {
        controller._Shoot();
    }
}
