using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeineMove1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //지니가 좌우로 움직이게 하고 싶다
    public GameObject geine;
    public GameObject startpoint;
    public GameObject endpoint;
    public float speed = 1;
    public AnimationCurve curve;
    float currentTime;

    float rotspeed = 50f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotspeed * Time.deltaTime, 0));
        currentTime += Time.deltaTime * speed;
        float t = curve.Evaluate(currentTime);
        //print(t);
        geine.transform.localPosition = Vector3.Lerp(startpoint.transform.localPosition, endpoint.transform.localPosition, t);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy3")
        {
            Instantiate(gameObject);
        }
    }

}
