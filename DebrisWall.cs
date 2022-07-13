using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 두번째 창문 파편 떨어지는 구간
// 플레이어가 창문에 부딪혔을 때 플레이어 위치에 폭발효과를 내고 싶다.
public class DebrisWall : MonoBehaviour
{
    public float forceG = 0.1f;
        
    //public GameObject windowDebrisFactory;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explosion()
    {
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].isKinematic = false;
            rigids[i].AddExplosionForce(forceG, transform.position, 10f);
        }
        //AudioManager.instance.PlaySFX("WindowDestroy");
        //WindowDestroy wdvfx = GetComponent<WindowDestroy>();
        //wdvfx.WindowDestroyVFX();
    }



   

}
