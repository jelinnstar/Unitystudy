using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어와 부딪히면 카펫들을 이동하라고 하고싶다.
public class ABox : MonoBehaviour
{
    public CarperMove[] carpets;
    //public CarperMove carpets1;
    //public CarperMove carpets2;
    //public CarperMove carpets3;

    public float speed = 5;


    private void OnTriggerEnter(Collider other)
    {
        print("111111111111111111111111111111111111111111");
        if (other.name.Contains("player"))
        {
            for (int i = 0; i < carpets.Length; i++)
            {
                carpets[i].isPlaying = true;
            }
        }
    }
}
