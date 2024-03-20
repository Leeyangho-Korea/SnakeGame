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

    // ���̵� �̹���
    [SerializeField]
    private Image fadeImage;

    // ���� ���� ������
    public bool isLive;

    // ��ư �ߺ� Ŭ������ ���� �ڷ�ƾ �ߺ� ���� ����
    private Coroutine co = null;

    // ���� ����
    private int score = 0;
    // ���� ���� �ؽ�Ʈ
    [Header("���� ���� ȭ�� UI")]
    [SerializeField]
    private TextMeshProUGUI text_Score;

    // ���� �ְ� ����
    private int rankScore = 0;
    // ���� �ְ� ���� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI text_RankScore;

    // ���� �ð� (��)
    private float gameTime = 60;
    // ���� ���� �ð� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI text_Time;

    // �÷��̾� �г���
    private string nickname = null;
    // �÷��̾� �г��� �ؽ�Ʈ
    [SerializeField]
    private TextMeshPro text_nickName;



    // ���� ���� �˾�
    [Header("���� ���� ȭ��UI")]
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
        // ���ӿ��� �ڷ�ƾ ����.
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        isLive = false;
        // �� ����
        text_UIscore.text = score.ToString();
        // ��ũ �������� ������
        if (score >= rankScore)
        {
            // �� �г��Ӱ� ��ũ���� ����.
            PlayerPrefs.SetInt("rankScore", score);
            PlayerPrefs.SetString("rankNickname", nickname);
            text_UIrankNickname.text = nickname;
            text_UIrankScore.text = score.ToString();
        }
        else
        {
            // �����ص״� �ְ� ���� �г���, ���� ���
            text_UIrankNickname.text = PlayerPrefs.GetString("rankNickname");
            text_UIrankScore.text = rankScore.ToString();
        }
        popup_GameOver.SetActive(true);
        yield return null;
    }

    // ���� ���� �гο��� �ٽ��ϱ� �Ǵ� �κ� ��ư ������ ��
    public void OnClickGameOverBtn(string sceneName)
    {
        if(co == null)
        {
            SoundManager.instance.SFXOneShot(0);
            co = StartCoroutine(SceneTrans(sceneName));
        }
    }

    // ������ ��ư ������ ��
    // ..���� ��� ����.
    public void OnClickGameExit()
    {
        SoundManager.instance.SFXOneShot(0);
        Application.Quit();
    }

    // �񵿱� �� ��ȯ���� ȭ�� ���̵� �ξƿ� ���� �ֱ�.
    private IEnumerator SceneTrans(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        fadeImage.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
    }
}
