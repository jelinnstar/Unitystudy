using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 정지했다가 Abox가 가라고하면 이동하고싶다.
// 이동방향과 크기를 미리 정해놓고싶다.
// 플레이어가 나랑 부딪히면 색을 바꾸고싶다.
// 플레이어가 떠나면 원래색으로 돌려놓고싶다.
public class CarperMove : MonoBehaviour
{
    public bool isPlaying;
    public float speed = 1;
    public Vector3 velocity;
    Color bodyColor;
    public Vector3 origin;

    void Start()
    {
        origin = transform.position;
        bodyColor = GetComponentInChildren<MeshRenderer>().material.color;
        this.isPlaying = false;
        velocity = -transform.forward * speed;
    }

    public void ChangeColor()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.cyan;
    }

    public void RestoreColor()
    {
        GetComponentInChildren<MeshRenderer>().material.color = bodyColor;

    }

    void Update()
    {
        if (isPlaying)
        {
            transform.position += velocity * Time.deltaTime;
        }
    }

    // 내가(카펫) 누군가와 부딪혔을 때
    private void OnTriggerEnter(Collider other)
    {
        //만약 플레이어라면
        if (other.name.Contains("player"))
        {
            // 카펫 이동 시작
            // 내색을 바꾸고싶다.
            this.ChangeColor();
            AudioManager.instance.PlaySFX("CarpetSelect");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 내가 누군가(플레이어)랑 부딪힘이 끝날 때
        if (other.name.Contains("player"))
        {
            // 카펫 이동 끝
            //isPlaying = false;
            this.RestoreColor();
        }
    }
}
