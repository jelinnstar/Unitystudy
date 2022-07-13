using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitZone1 : MonoBehaviour
{
    public Transform SecondPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("player"))
        {
            print("QuitZone1 : " + SecondPosition.name);
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.transform.position = SecondPosition.position;
            other.transform.rotation = SecondPosition.rotation;
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
