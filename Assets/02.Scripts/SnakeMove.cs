using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    float handle = 0f;

    void Start()
    {

    }

    void Update()
    {
        handle = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);

        transform.Rotate(-Vector3.forward * handle * rotationSpeed * Time.deltaTime);
    }
}
