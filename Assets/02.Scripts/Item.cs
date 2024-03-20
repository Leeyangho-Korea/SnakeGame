using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float moveRadius = 10f; // �̵� �ݰ�

    // ������ �� Ȱ��ȭ ��ų ������Ʈ
    public bool isStartEnable;

    private void Start()
    {
        // ������ �� Ȱ��ȭ ���� ������ ���� ������Ʈ ��Ȱ��ȭ
        if (!isStartEnable)
            GameManager.instance.itemManager.DisableItem(this.gameObject);
        else // ������ �� Ȱ��ȭ �ȴٸ� ������ ���ġ
            OnEnable();
    }

    private void OnEnable()
    {
        // �θ� ������Ʈ�� Transform ������Ʈ�� ������
        Transform parentTransform = transform.parent;

        // �θ� ������Ʈ�� ũ�� (���� ������)�� ������
        Vector3 parentScale = parentTransform.localScale;

        // �θ� ������Ʈ�� ũ�⸦ �ݿ��Ͽ� �̵� �ݰ� ���
        float adjustedMoveRadius = moveRadius * Mathf.Max(parentScale.x, parentScale.y, parentScale.z);

        // ������ ��ġ�� �̵�
        Vector3 randomPosition = parentTransform.position + Random.insideUnitSphere * adjustedMoveRadius;

        // z�� ���� �θ� ������Ʈ�� z�� ������ �����Ͽ� 2D ��鿡���� ���� �̵� ����
        randomPosition.z = parentTransform.position.z;

        // �ڽ��� ��ġ�� ������ ��ġ�� ����
        transform.position = randomPosition;
    }
}
