using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform snakeTail;
    public float circleDiameter;

    private List<Transform> list_SnakeTail = new List<Transform>();
    private List<Vector2> list_Position = new List<Vector2>();

    void Start()
    {
        list_Position.Add(snakeTail.position);
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        // 머리와 꼬리 사이의 거리 계산
        float distance = ((Vector2)snakeTail.position - list_Position[0]).magnitude;

        // 머리와 꼬리 사이의 거리가 원의 지름보다 큰 경우
        if (distance > circleDiameter)
        {
            // 머리에서 꼬리 방향으로의 벡터 계산
            Vector2 direction = ((Vector2)snakeTail.position - list_Position[0]).normalized;

            // 새 위치를 계산하여 리스트의 맨 앞에 삽입
            list_Position.Insert(0, list_Position[0] + direction * circleDiameter);

            // 리스트의 마지막 위치를 제거하여 꼬리를 이동
            list_Position.RemoveAt(list_Position.Count - 1);

            // 이동 거리 갱신
            distance -= circleDiameter;
        }

        //  현재 꼬리들을 순회
        for (int i = 0; i < list_SnakeTail.Count; i++)
        {
            // 현재 꼬리의 위치를 이전 위치와 현재 윛치 사이에서 선형 보간
            list_SnakeTail[i].position = Vector2.Lerp(list_Position[i + 1], list_Position[i], distance / circleDiameter);
        }
    }
    
    private void AddTail()
    {
        // 꼬리 추가 생성 ( 현재 꼬리의 마지막 꼬리 위치로 )
        Transform tail = Instantiate(snakeTail, list_Position[list_Position.Count - 1], Quaternion.identity, transform);
        list_SnakeTail.Add(tail);
        list_Position.Add(tail.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        if (collision.CompareTag("Gem"))
        {
            SoundManager.instance.SFXOneShot(1);
            GameManager gameManger = GameManager.instance;
            // 아이템 비활성화
            gameManger.itemManager.DisableItem(collision.gameObject);
            // 점수 증가
            gameManger.ScoreUp();
            // 꼬리 증가
            AddTail();
        }
        else if(collision.CompareTag("Feather"))
        {
            SoundManager.instance.SFXOneShot(2);
            // 스피드 일시 증가
            GetComponent<SnakeMove>().SpeedUp();
            // 아이템 비활성화
            GameManager.instance.itemManager.DisableItem(collision.gameObject);
        }
    }
}
