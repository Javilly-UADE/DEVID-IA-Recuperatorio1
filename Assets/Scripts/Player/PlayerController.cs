using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerModel model;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        model.Walk(dir);
        if (horizontal != 0 || vertical != 0)
        {
            model.Rotate(dir);
        }
    }
}
