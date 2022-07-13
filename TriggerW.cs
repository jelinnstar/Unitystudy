using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerW : MonoBehaviour
{
    public DebrisWall debris;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("player"))
        {
            print("ccccccccccccccccccccc");
            debris.Explosion();
            AudioManager.instance.PlaySFX("WindowDestroy");
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
