using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float moveRadius = 10f; // 이동 반경

    // 시작할 때 활성화 시킬 오브젝트
    public bool isStartEnable;

    private void Start()
    {
        // 시작할 때 활성화 하지 않으면 게임 오브젝트 비활성화
        if (!isStartEnable)
            GameManager.instance.itemManager.DisableItem(this.gameObject);
        else // 시작할 때 활성화 된다면 아이템 재배치
            OnEnable();
    }

    private void OnEnable()
    {
        // 부모 오브젝트의 Transform 컴포넌트를 가져옴
        Transform parentTransform = transform.parent;

        // 부모 오브젝트의 크기 (로컬 스케일)를 가져옴
        Vector3 parentScale = parentTransform.localScale;

        // 부모 오브젝트의 크기를 반영하여 이동 반경 계산
        float adjustedMoveRadius = moveRadius * Mathf.Max(parentScale.x, parentScale.y, parentScale.z);

        // 랜덤한 위치로 이동
        Vector3 randomPosition = parentTransform.position + Random.insideUnitSphere * adjustedMoveRadius;

        // z축 값은 부모 오브젝트의 z축 값으로 설정하여 2D 평면에서의 랜덤 이동 보장
        randomPosition.z = parentTransform.position.z;

        // 자신의 위치를 랜덤한 위치로 설정
        transform.position = randomPosition;
    }
}
