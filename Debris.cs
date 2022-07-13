using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 첫번째 debris 구간
public class Debris : MonoBehaviour
{
    public float forceG = 0.1f;

    void Start()
    {

    }

    void Update()
    {

    }

    public GameObject debrisFactory;
    public void Explosion()
    {
        GameObject debris = Instantiate(debrisFactory);
        Rigidbody[] rigids = debris.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].isKinematic = false;
            rigids[i].AddExplosionForce(forceG, transform.position, 10f);
        }
        Destroy(debris, 10);
        AudioManager.instance.PlaySFX("DebrisDestroy");
    }

}
