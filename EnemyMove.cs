using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//태어날때 cube를 startwall에 놓고 싶다.
//cube가 endwall에 갔다가 startwall로 가기를 반복하고 싶다
public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public GameObject Box;
    public GameObject Box2;
    public GameObject Box3;
    public GameObject Box4;
    public GameObject startwall;
    public GameObject endwall;
    public GameObject startwall2;
    public GameObject endwall2;
    public GameObject startwall3;
    public GameObject endwall3;
    public GameObject startwall4;
    public GameObject endwall4;

    public float speed = 1;
    public AnimationCurve curve;
    float currentTime;

// Update is called once per frame
void Update()
    {
        currentTime += Time.deltaTime * speed;
        float t = curve.Evaluate(currentTime);
        //print(t);
        Box.transform.localPosition = Vector3.Lerp(startwall.transform.localPosition, endwall.transform.localPosition, t);
        Box2.transform.localPosition = Vector3.Lerp(startwall2.transform.localPosition, endwall2.transform.localPosition, t);
        Box3.transform.localPosition = Vector3.Lerp(startwall3.transform.localPosition, endwall3.transform.localPosition, t);
        Box4.transform.localPosition = Vector3.Lerp(startwall4.transform.localPosition, endwall4.transform.localPosition, t);


    }
}
