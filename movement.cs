using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float moveForce = 10f;     // сила разгона
    public float jumpForce = 6f;      // сила прыжка
    public float maxSpeed = 8f;       // ограничение горизонтальной скорости
    public LayerMask groundMask = -1; // какие слои считать землёй

    private Rigidbody rb;
    private float radius;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        radius = GetComponent<SphereCollider>().radius * transform.localScale.x;
    }

    void Update()
    {
        // Ввод прыжка ловим в Update, чтобы не пропустить нажатие
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Движение относительно камеры
        Transform cam = Camera.main.transform;
        Vector3 forward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Vector3.Scale(cam.right, new Vector3(1, 0, 1)).normalized;
        Vector3 dir = (forward * v + right * h);

        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude < maxSpeed)
            rb.AddForce(dir * moveForce, ForceMode.Force);

        if (jumpPressed && IsGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        jumpPressed = false;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, radius + 0.1f, groundMask);
    }
}