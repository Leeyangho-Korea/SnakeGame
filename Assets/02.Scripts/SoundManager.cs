using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("#BGM")]
    // BGM을 실행시켜줄 AudioClip
    [SerializeField]
    private AudioSource bgmSource;

    [Header("#SFX")]
    // 효과음 AudioSource
    [SerializeField]
    private AudioSource sfxSource;
    // 효과음 배열
    [SerializeField]
    public AudioClip[] sfxClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void SFXOneShot(int i)
    {
        sfxSource.PlayOneShot(sfxClips[i]);
    }
}
