using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // inspectorâ���� ���������ϰ� ��
public class Sound   // Sound ������ ��� �ִ� Ŭ����
{
    public string name;   // ���� �̸�
    public AudioClip clip;   // ���� mp3������ ���⿡ ������ ��
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Start()
    {
        instance = this;
    }

    [SerializeField] Sound bgm;
    [SerializeField] Sound goalBgm;
    [SerializeField] Sound[] sfx;

    [SerializeField] AudioSource bgmPlayer;
    [SerializeField] AudioSource goalBgmPlayer;
    [SerializeField] AudioSource[] sfxPlayer;

    public void PlayBGM() // �Ű����� p_bgmName�� �̸��� SoundŬ������ ���� name�� ��ġ�ϴ��� ���ϱ� ���ؼ� for���� ����ؾ���
    {
        goalBgmPlayer.Stop();
        bgmPlayer.Play();
    }

    public void PlayGoalBGM() // �Ű����� p_bgmName�� �̸��� SoundŬ������ ���� name�� ��ġ�ϴ��� ���ϱ� ���ؼ� for���� ����ؾ���
    {
        goalBgmPlayer.Play();
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    sfxPlayer[x].clip = sfx[i].clip;
                    sfxPlayer[x].Play();
                }
            }
        }
    }


    /* public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfx[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                return;
            }
        }
    } */

    // Update is called once per frame
    void Update()
    {

    }
}
