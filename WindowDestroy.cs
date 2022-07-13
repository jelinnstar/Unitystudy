using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDestroy : MonoBehaviour
{
    public GameObject windowDebrisFactory;

    public void WindowDestroyVFX()
    {
        GameObject wd = Instantiate(windowDebrisFactory);
        wd.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("player"))
        {
            WindowDestroy wdvfx = GetComponent<WindowDestroy>();
            wdvfx.WindowDestroyVFX();
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
