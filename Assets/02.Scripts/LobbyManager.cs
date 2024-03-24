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

    //..테스트용 데이터 삭제여부
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
        // 버튼 중복 클릭으로 인한 코루틴 중복방지
        if (co == null)
        {
            SoundManager.instance.SFXOneShot(0);
               co = StartCoroutine(SceneTrans("GameScene"));
        }
    }

    // 비동기 씬 전환으로 화면 페이드 인아웃 연출 주기.
    private IEnumerator SceneTrans(string sceneName)
    {
        // 닉네임 수정 제한
        input_Nickname.interactable = false;
        // 닉네임 세팅
        PlayerPrefs.SetString("Nickname", input_Nickname.text);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        fadeImage.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
    }
}
