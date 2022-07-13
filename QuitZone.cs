using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitZone : MonoBehaviour
{
    public Transform startPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("player"))
        {
            //print("QuitZone : " + startPosition.name + ", myName : " + gameObject.name);
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.transform.position = startPosition.position;
            other.transform.rotation = startPosition.rotation;
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
