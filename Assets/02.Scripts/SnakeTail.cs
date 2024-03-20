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
        // �Ӹ��� ���� ������ �Ÿ� ���
        float distance = ((Vector2)snakeTail.position - list_Position[0]).magnitude;

        // �Ӹ��� ���� ������ �Ÿ��� ���� �������� ū ���
        if (distance > circleDiameter)
        {
            // �Ӹ����� ���� ���������� ���� ���
            Vector2 direction = ((Vector2)snakeTail.position - list_Position[0]).normalized;

            // �� ��ġ�� ����Ͽ� ����Ʈ�� �� �տ� ����
            list_Position.Insert(0, list_Position[0] + direction * circleDiameter);

            // ����Ʈ�� ������ ��ġ�� �����Ͽ� ������ �̵�
            list_Position.RemoveAt(list_Position.Count - 1);

            // �̵� �Ÿ� ����
            distance -= circleDiameter;
        }

        //  ���� �������� ��ȸ
        for (int i = 0; i < list_SnakeTail.Count; i++)
        {
            // ���� ������ ��ġ�� ���� ��ġ�� ���� ��ġ ���̿��� ���� ����
            list_SnakeTail[i].position = Vector2.Lerp(list_Position[i + 1], list_Position[i], distance / circleDiameter);
        }
    }
    
    private void AddTail()
    {
        // ���� �߰� ���� ( ���� ������ ������ ���� ��ġ�� )
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
            // ������ ��Ȱ��ȭ
            gameManger.itemManager.DisableItem(collision.gameObject);
            // ���� ����
            gameManger.ScoreUp();
            // ���� ����
            AddTail();
        }
        else if(collision.CompareTag("Feather"))
        {
            SoundManager.instance.SFXOneShot(2);
            // ���ǵ� �Ͻ� ����
            GetComponent<SnakeMove>().SpeedUp();
            // ������ ��Ȱ��ȭ
            GameManager.instance.itemManager.DisableItem(collision.gameObject);
        }
    }
}
