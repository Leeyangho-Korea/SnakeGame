using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] 
    Image fadeImage;

    [SerializeField]
    TMP_InputField input_Nickname;

    [SerializeField]
    Button btn_Start;

    private Coroutine co = null;

    //..�׽�Ʈ�� ������ ��������
    public bool deleteData;

    private void Awake()
    {
        if (deleteData)
            PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(0f, 1f);
    }

   public void OnInputFieldValueChanged()
    {
        if (input_Nickname.text.Length > 0)
            btn_Start.interactable = true;
        else
            btn_Start.interactable = false;
    }

    public void OnClickBtnStart()
    {
        // ��ư �ߺ� Ŭ������ ���� �ڷ�ƾ �ߺ�����
        if (co == null)
        {
            SoundManager.instance.SFXOneShot(0);
               co = StartCoroutine(SceneTrans("GameScene"));
        }
    }

    // �񵿱� �� ��ȯ���� ȭ�� ���̵� �ξƿ� ���� �ֱ�.
    private IEnumerator SceneTrans(string sceneName)
    {
        // �г��� ���� ����
        input_Nickname.interactable = false;
        // �г��� ����
        PlayerPrefs.SetString("Nickname", input_Nickname.text);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        fadeImage.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
    }
}
