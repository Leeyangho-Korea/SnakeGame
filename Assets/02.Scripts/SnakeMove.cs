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
        // 좌 우 키 입력 받아기
        handle = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        // 로컬 좌표 기준에서 위쪽으로 이동 ( 앞으로 이동 )
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);

        // 좌 우 키 입력 받은 값으로 왼쪽 오른쪽 방향 전환.
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
