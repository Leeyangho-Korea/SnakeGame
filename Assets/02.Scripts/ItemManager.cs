using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefab_Item;
    // 젠 타임
    [SerializeField] List<float> genTIme_Item;
    // 비활성화 돼 있는 아이템들
    public List<GameObject> disabled_Item;
    // 젠 관리 타임
    private float now_Time = 0f;

    void Start()
    {
        genTIme_Item = new List<float>();
        for(int i = 0; i < disabled_Item.Count; i ++)
        {
            genTIme_Item.Add(Random.Range(2f, 5f));
        }
    }

   
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        if (disabled_Item.Count >0)
        {
            EnableItem();
        }
    }

    private void EnableItem()
    {
        now_Time += Time.deltaTime;
        for (int i = 0; i < disabled_Item.Count; i++)
        {
            if (genTIme_Item[i] < now_Time)
            {
                disabled_Item[i].SetActive(true);
                genTIme_Item.RemoveAt(i);
                disabled_Item.RemoveAt(i);
                // 아이템 활성화 이후에도 비활성화 된 아이템들이 많다면
                if (disabled_Item.Count > 0)
                {
                    i--;
                }
            }
        }
    }

    public void DisableItem(GameObject _gem)
    {
        _gem.SetActive(false);
        disabled_Item.Add(_gem);
        genTIme_Item.Add(Random.Range(2f, 5f));
        // 젠 관리 타임 초기화
        now_Time = 0f;
    }
}
