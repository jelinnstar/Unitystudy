using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject debrisWall;
    public GameObject windowDestroy;
    public GameObject triggerW;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("player"))
        {
            debrisWall.SetActive(false);
            windowDestroy.SetActive(false);
            triggerW.SetActive(false);
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
