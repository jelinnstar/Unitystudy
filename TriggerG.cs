using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerG : MonoBehaviour
{
    public Debris debris;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("player"))
        {
            print("aaaaaaaaaaaaaaaaa");
            debris.Explosion();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
