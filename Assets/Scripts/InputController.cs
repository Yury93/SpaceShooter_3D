using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Joystick joystickRotate,joistickMove;
    [SerializeField] private float speed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        var directMove = new Vector3(joistickMove.Horizontal, joystickRotate.Vertical, joistickMove.Vertical);
        rb.AddForce(directMove * speed * Time.deltaTime, ForceMode.Impulse);

        var directRotate = new Vector3(0, 0, joystickRotate.Horizontal);
        rb.AddTorque(directRotate * speed * Time.deltaTime, ForceMode.Impulse);
    }
}
