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

        float diffX = playerPos.x - myPos.x;
        float diffY = playerPos.y - myPos.y;
        float dirX = diffX < 0 ? -1 : 1;
        float dirY = diffY < 0 ? -1 : 1;
        diffX = Mathf.Abs(diffX);
        diffY = Mathf.Abs(diffY);

        if (diffX > diffY)
        {
            transform.Translate(Vector3.right * dirX * 40);
        }
        else if (diffX < diffY)
        {
            transform.Translate(Vector3.up * dirY * 40);
        }

    }
}
