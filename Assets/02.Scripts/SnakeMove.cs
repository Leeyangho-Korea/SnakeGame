using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    float handle = 0f;
    private Coroutine co = null;

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        // �� �� Ű �Է� �޾Ʊ�
        handle = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        // ���� ��ǥ ���ؿ��� �������� �̵� ( ������ �̵� )
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);

        // �� �� Ű �Է� ���� ������ ���� ������ ���� ��ȯ.
        transform.Rotate(-Vector3.forward * handle * rotationSpeed * Time.fixedDeltaTime);
    }

    public void SpeedUp()
    {
        if(co != null)
        {
            StopCoroutine(co);
        }
        co = StartCoroutine(TransSpeed());
    }

    private IEnumerator TransSpeed()
    {
        speed += 3;
        yield return new WaitForSeconds(3f);
        speed -= 3;
        co = null;
    }
}
