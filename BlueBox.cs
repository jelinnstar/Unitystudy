using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBox : MonoBehaviour
{

    public enum State
    {
        walk,
        take_damage
    }
    public State state;
    public Animator ani;

    //파란박스가 좌우로 움직이게 하고 싶다
    public GameObject bluebox;
    public GameObject startpoint;
    public GameObject endpoint;
    public float speed = 1;
    public AnimationCurve curve;
    float currentTime;

    CharacterController bc;

    float rotspeed = 50f;
    float curvetime;


    //건달이 게임 시작에 움직였으면 좋겠다.
    //건달이 엔드지점에 도달하면 돌아서 다시 시작점으로 갔으면 좋겠다.
    void Start()
    {
        bc = GetComponent<CharacterController>();
        state = State.walk;
        ani.SetTrigger("walk");
    }

    // Update is called once per frame
    void Update()
    {
        //만약 건달이 엔드지점에 도달하면
        //돌아서 다시 시작점으로 갔으면 좋겠다
        if (this.transform.position == endpoint.transform.localPosition)
        {
            transform.Rotate(new Vector3(0, rotspeed * Time.deltaTime, 0));
            this.transform.position = startpoint.transform.localPosition;
            return;
        }



        transform.Rotate(new Vector3(0, rotspeed * Time.deltaTime, 0));
        currentTime += Time.deltaTime * speed;
        float t = curve.Evaluate(currentTime);

        //print(t);
        bluebox.transform.localPosition = Vector3.Lerp(startpoint.transform.localPosition, endpoint.transform.localPosition, t);

    }

    private void OnCollisionEnter(Collision collision)
    {
        state = State.take_damage;
        return;
    }

}