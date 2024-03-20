using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;

    // 페이드 이미지
    [SerializeField]
    private Image fadeImage;

    // 게임 진행 중인지
    public bool isLive;

    // 버튼 중복 클릭으로 인한 코루틴 중복 실행 방지
    private Coroutine co = null;

    // 꼬리 개수
    private int score = 0;
    // 꼬리 개수 텍스트
    [Header("게임 중인 화면 UI")]
    [SerializeField]
    private TextMeshProUGUI text_Score;

    // 이전 최고 점수
    private int rankScore = 0;
    // 이전 최고 점수 텍스트
    [SerializeField]
    private TextMeshProUGUI text_RankScore;

    // 게임 시간 (초)
    private float gameTime = 60;
    // 남은 게임 시간 텍스트
    [SerializeField]
    private TextMeshProUGUI text_Time;

    // 플레이어 닉네임
    private string nickname = null;
    // 플레이어 닉네임 텍스트
    [SerializeField]
    private TextMeshPro text_nickName;



    // 게임 종료 팝업
    [Header("게임 종료 화면UI")]
    [SerializeField]
    private GameObject popup_GameOver;
    [SerializeField]
    private TextMeshProUGUI text_UIrankNickname, text_UIrankScore, text_UIscore;



    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        nickname = PlayerPrefs.GetString("Nickname");
        rankScore = PlayerPrefs.GetInt("rankScore");
        text_nickName.text = nickname;
        text_RankScore.text = rankScore.ToString();
    }

    void Start()
    {
        int min = Mathf.FloorToInt(gameTime / 60);
        int sec = Mathf.FloorToInt(gameTime % 60);
        text_Time.text = string.Format("{0:D2}:{1:D2}", min, sec);
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(0f, 1f);
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            GameOver();
        }
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        int min = Mathf.FloorToInt(gameTime / 60);
        int sec = Mathf.FloorToInt(gameTime % 60);
        text_Time.text = string.Format("{0:D2}:{1:D2}", min, sec);
    }

    public void ScoreUp()
    {
        score += 1;
        text_Score.text = score.ToString();
    }

    public void GameOver()
    {
        // 게임오버 코루틴 실행.
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        isLive = false;
        // 내 점수
        text_UIscore.text = score.ToString();
        // 랭크 점수보다 높으면
        if (score >= rankScore)
        {
            // 내 닉네임과 랭크점수 저장.
            PlayerPrefs.SetInt("rankScore", score);
            PlayerPrefs.SetString("rankNickname", nickname);
            text_UIrankNickname.text = nickname;
            text_UIrankScore.text = score.ToString();
        }
        else
        {
            // 저장해뒀던 최고 점수 닉네임, 점수 출력
            text_UIrankNickname.text = PlayerPrefs.GetString("rankNickname");
            text_UIrankScore.text = rankScore.ToString();
        }
        popup_GameOver.SetActive(true);
        yield return null;
    }

    // 게임 오버 패널에서 다시하기 또는 로비 버튼 눌렀을 때
    public void OnClickGameOverBtn(string sceneName)
    {
        if(co == null)
        {
            SoundManager.instance.SFXOneShot(0);
            co = StartCoroutine(SceneTrans(sceneName));
        }
    }

    // 나가기 버튼 눌렀을 때
    // ..현재 사용 안함.
    public void OnClickGameExit()
    {
        SoundManager.instance.SFXOneShot(0);
        Application.Quit();
    }

    // 비동기 씬 전환으로 화면 페이드 인아웃 연출 주기.
    private IEnumerator SceneTrans(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        fadeImage.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
    }
}
