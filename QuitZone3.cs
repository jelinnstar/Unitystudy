using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitZone3 : MonoBehaviour
{
    public Transform FourthPosition;
    public Transform carpetEnded;
    public CarperMove[] carpets;
    public GameObject Carpets;
    Vector3 dir;


    //내가 플레이어랑 만나면
    private void OnTriggerEnter(Collider other)
    {
        // 만약 플레이어와 부딪혔다면..
        if (other.name.Contains("player"))
        {
            print("QuitZone3 :: OnTriggerEnter : " + other.name);
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.transform.position = FourthPosition.position;
            other.transform.rotation = FourthPosition.rotation;
            player p = other.gameObject.GetComponent<player>();
            p.MakeTeleportVFX();
            other.gameObject.GetComponent<CharacterController>().enabled = true;
            Invoke(nameof(ResetCarpet), 2); // 2초뒤 DelayCarpet 호출
        }

    }

    // 카펫들 위치를 초기로 옮긴다
    void ResetCarpet()
    {
        print("ResetCarpet");
        for (int i = 0; i < carpets.Length; i++)
        {
            //몇 초 후에

            //카펫들이 처음 위치로 리셋하라고 하고 싶다.
            carpets[i].transform.position = carpets[i].origin;
            // 카펫이 안 움직이게 설정
            carpets[i].isPlaying = false;
        }
    }

    void Start()
    {
    }

}
