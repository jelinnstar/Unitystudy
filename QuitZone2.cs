using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitZone2 : MonoBehaviour
{
    public Transform ThirdPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("player"))
        {
            print("QuitZone2 : " + ThirdPosition.name);
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.transform.position = ThirdPosition.position;
            other.transform.rotation = ThirdPosition.rotation;
            player p = other.gameObject.GetComponent<player>();
            p.MakeTeleportVFX();
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
