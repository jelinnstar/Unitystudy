using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // inspector창에서 수정가능하게 함
public class Sound   // Sound 정보를 담고 있는 클래스
{
    public string name;   // 사운드 이름
    public AudioClip clip;   // 실제 mp3음원을 여기에 넣으면 됨
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

    public void PlayBGM() // 매개변수 p_bgmName의 이름이 Sound클래스의 변수 name과 일치하는지 비교하기 위해서 for문을 사용해야함
    {
        goalBgmPlayer.Stop();
        bgmPlayer.Play();
    }

    public void PlayGoalBGM() // 매개변수 p_bgmName의 이름이 Sound클래스의 변수 name과 일치하는지 비교하기 위해서 for문을 사용해야함
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
