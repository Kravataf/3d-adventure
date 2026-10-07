using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Range(0.01f,10f)][SerializeField] private float sens = 1f;
    [SerializeField] private Transform body;

    private Vector2 current, target, rot, vel;
    private InputAction look;

    private void Awake()
    {
        look = InputSystem.actions["Look"];
    }

    private void Update()
    {
        if (UIManager.instance.isPaused || UIManager.instance.currentState != State.Focus) return;

        current = new Vector2(
            target.x + Mathf.DeltaAngle(target.x, body.localEulerAngles.y),
            -Mathf.DeltaAngle(0f, transform.localEulerAngles.x)
        );

        target += look.ReadValue<Vector2>() * sens;
        target.y = Mathf.Clamp(target.y, -90f, 90f);

        rot = Vector2.SmoothDamp(current, target, ref vel, 0.025f);

        body.gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0f, rot.x, 0f));
        transform.localRotation = Quaternion.Euler(-rot.y, 0f, 0f);
    }
}
