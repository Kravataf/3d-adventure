using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float wishSpeed, jumpForce, groundAccel, airAccel, friction, maxGroundSpeed, maxAirSpeed;
    [SerializeField] public Rigidbody rb;
    private Vector2 inputDir;
    private Vector3 wishDir;

    [HideInInspector] public bool grounded;
    [HideInInspector] public float hSpeed => new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;
    [HideInInspector] public float vSpeed => rb.linearVelocity.y;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        inputDir = InputSystem.actions["Move"].ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 feet = transform.position - new Vector3(0, 1, 0);
        grounded = Physics.CheckSphere(feet, .01f, ~(1 << gameObject.layer)) && GetSlopeAngle() < 40f;

        wishDir = transform.TransformDirection(new Vector3(inputDir.x, 0f, inputDir.y));

        if (grounded)
        {
            ApplyFriction();
            Accelerate(maxGroundSpeed, groundAccel);

            if (InputSystem.actions["Jump"].IsPressed()) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            wishDir = GetSlopeDirection();
            Accelerate(maxAirSpeed, airAccel);
        }
    }

    void Accelerate(float maxSpeed, float accel)
    {
        float dot = Vector3.Dot(rb.linearVelocity, wishDir);
        if (dot >= maxSpeed) return;

        float add = Mathf.Clamp(wishSpeed - dot, 0f, accel * Time.fixedDeltaTime);
        rb.linearVelocity += wishDir * add;
    }

    void ApplyFriction()
    {
        float currentSpd = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;
        if (currentSpd <= 0) return;

        float drop = currentSpd * friction * Time.fixedDeltaTime;
        float newSpd = Mathf.Max((currentSpd - drop) / currentSpd, 0f);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x * newSpd, rb.linearVelocity.y, rb.linearVelocity.z * newSpd);
    }

    public float GetSlopeAngle()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f, ~(1 << gameObject.layer)))
            return Mathf.Round(Vector3.Angle(hit.normal, Vector3.up));
        return 0f;
    }

    Vector3 GetSlopeDirection()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f, ~(1 << gameObject.layer)))
            return Vector3.ProjectOnPlane(wishDir, hit.normal).normalized;
        return wishDir;
    }
}