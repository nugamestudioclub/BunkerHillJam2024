using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController controller;
    public static PlayerController Instance { get { return controller; } }
    private CharacterController player;

    [SerializeField]
    private static float GRAVITY = 0.15f;
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

    Vector3 originalHitboxSize;

    private Vector2 vel = Vector2.zero;
    private bool facingRight = true;
    private bool isCrouching = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        player = GetComponent<CharacterController>();
        originalHitboxSize = player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateController();

        UpdateDirection();
    }

    private void UpdateController()
    {
        LateralMovement();
        vel.x = Mathf.Lerp(vel.x, 0, Time.deltaTime * FRICTION);

        vel.y -= GRAVITY * Time.deltaTime;

        if (player.isGrounded)
        {
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
            facingRight = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x -= NORMAL_MOVE_SPEED * Time.deltaTime;
            facingRight = false;
        }
    }

    public static bool IsGrounded()
    {
        controller.player.TryGetComponent<CharacterController>(out var c);
        //Debug.Log(Physics.Raycast(controller.player.transform.position, Vector2.down).collider);
        RaycastHit hit;
        Ray r = new Ray();
        r.origin = controller.player.transform.position;
        r.direction = Vector2.down;
        if(Physics.Raycast(r,out hit, c.height / 2 + 0.1f, controller.l))
        {
            print(hit.point + "," + hit.transform.name);
            return true;
        }
        return false;
    }

    public static void Jump()
    {
        Debug.Log("jumping now!");
        controller.vel.y = PlayerController.IsCrouching() ? NORMAL_JUMP_HEIGHT * 0.75f : NORMAL_JUMP_HEIGHT;
        controller.player.Move(Vector3.up * controller.vel.y);
    }

    private void _Shoot()
    {
        GameObject proj = Instantiate(firePrefab);
        proj.transform.parent = null;
        
        proj.transform.position = this.transform.position + (Vector3.right * lastDir / Mathf.Abs(lastDir));
        proj.GetComponent<Projectile>().SetDirection(lastDir/Mathf.Abs(lastDir));
    }

    // private
    private void _Crouch()
    {
        isCrouching = true;
        player.transform.localScale = player.transform.localScale + Vector3.down * 0.25f;
        player.GetComponent<CharacterController>().height /= 2;
        player.GetComponent<CharacterController>().radius /= 2;
    }

    // private
    private void _Uncrouch()
    {
        isCrouching = false;
        player.transform.localScale = originalHitboxSize;
        player.GetComponent<CharacterController>().height *= 2f;
        player.GetComponent<CharacterController>().radius *= 2f;
    }

    // private
    private void _Dash()
    {
        if (facingRight)
        {
            vel.x = NORMAL_MOVE_SPEED;
        }
        else
        {
            vel.x = -NORMAL_MOVE_SPEED;
        }
    }
    public static void Crouch()
    {
        controller._Crouch();
    }

    public static void Uncrouch()
    {
        controller._Uncrouch();
    }

    public static bool IsCrouching()
    {
        return controller.isCrouching;
    }

    public static void Dash()
    {
        controller._Dash();
    }

    public static void Shoot()
    {
        controller._Shoot();
    }
}
