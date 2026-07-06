using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Walk(Vector3 dir)
    {
        rb.linearVelocity = dir * speed;
    }

    public void Rotate(Vector3 dir)
    {
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotationSpeed);
    }
}
