using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("#BGM")]
    // BGM�� ��������� AudioClip
    [SerializeField]
    private AudioSource bgmSource;

    [Header("#SFX")]
    // ȿ���� AudioSource
    [SerializeField]
    private AudioSource sfxSource;
    // ȿ���� �迭
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
