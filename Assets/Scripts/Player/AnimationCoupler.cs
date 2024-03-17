using UnityEngine;

public class AnimationCoupler : MonoBehaviour
{
    private Animator m_animator;
    private SpriteRenderer m_renderer;

    private int m_absVelXHash;
    private int m_velYHash;
    private int m_groundedHash;
    private int m_crouchingHash;
    private int m_fireHash;
    private int m_dashHash;
    private int m_damageHash;

    private bool m_wasGrounded;
    private bool m_wasCrouching;

    [SerializeField]
    private CharacterController m_observedCharacterController;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_renderer = GetComponent<SpriteRenderer>();

        m_absVelXHash = Animator.StringToHash("abs_vel_x");
        m_velYHash = Animator.StringToHash("vel_y");
        m_groundedHash = Animator.StringToHash("grounded");
        m_crouchingHash = Animator.StringToHash("crouching");
        m_fireHash = Animator.StringToHash("fire");
        m_dashHash = Animator.StringToHash("dash");
        m_damageHash = Animator.StringToHash("damage");
    }

    private void Start()
    {
        PlayerController.Instance.OnFire += PrimeFireTrigger;
        PlayerController.Instance.OnDash += PrimeDashTrigger;
    }

    private void OnDestroy()
    {
        PlayerController.Instance.OnFire -= PrimeFireTrigger;
        PlayerController.Instance.OnDash -= PrimeDashTrigger;
    }

    private void Update()
    {
        var current_velocity = m_observedCharacterController.velocity;

        if (current_velocity.x != 0) m_renderer.flipX = current_velocity.x < 0;

        m_animator.SetFloat(m_absVelXHash, Mathf.Abs(current_velocity.x));
        m_animator.SetFloat(m_velYHash, current_velocity.y);

        bool is_crouching = PlayerController.IsCrouching();
        bool is_grounded = PlayerController.IsGrounded();

        if (m_wasCrouching != is_crouching)
        {
            m_wasCrouching = is_crouching;
            m_animator.SetBool(m_crouchingHash, is_crouching);
        }

        if (m_wasGrounded != is_grounded)
        {
            m_wasGrounded = is_grounded;
            m_animator.SetBool(m_groundedHash, is_grounded);
        }
    }

    public void ConditionalPrimeDamageTrigger(int amount, int _)
    {
        if (amount >= 0) return;

        m_animator.SetTrigger(m_damageHash);
    }

    public void PrimeDashTrigger() => m_animator.SetTrigger(m_dashHash);
    public void PrimeFireTrigger() => m_animator.SetTrigger(m_fireHash);
}
