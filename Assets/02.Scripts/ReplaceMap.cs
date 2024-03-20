using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타일맵 이동 스크립트 ( 무한 맵 ) 
/// </summary>

public class ReplaceMap : MonoBehaviour
{

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = collision.transform.position;
        Vector3 myPos = transform.position;

        // diffX = 현재 X축으로의 거리 ( 0보다 크다면 타일맵 기준 오른쪽으로 이동)
        float diffX = playerPos.x - myPos.x;
        // diffY = 현재 Y축으로의 거리 ( 0 보다 크다면 타일맵 기준 위쪽으로 이동 )
        float diffY = playerPos.y - myPos.y;
        float dirX = diffX < 0 ? -1 : 1;
        float dirY = diffY < 0 ? -1 : 1;
        // 절댓값 계산
        diffX = Mathf.Abs(diffX);
        diffY = Mathf.Abs(diffY);

        // x축으로의 이동이 y 축으로의 이동보다 크다면
        if (diffX > diffY)
        {
            // x로 이동한 곳으로 (dirX가 -1일 경우 왼쪽, 1일경우 오른쪽) 맵 이동
            transform.Translate(Vector3.right * dirX * 40);
        }
        // y축으로의 이동이 x축으로의 이동보다 크다면
        else if (diffX < diffY)
        {
            // x로 이동한 곳으로 (dirY가 -1일 경우 아래쪽, 1일경우 위쪽) 맵 이동
            transform.Translate(Vector3.up * dirY * 40);
        }

    }

}
