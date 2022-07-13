using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    AudioSource runSfx;
    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponentInParent<CharacterController>();
        runSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playerFootSound()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        if (cc.isGrounded || Physics.Raycast(ray, out hitInfo, 0.5f))
        {
            runSfx.Play();
        }
    }
}
