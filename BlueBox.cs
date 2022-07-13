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

    //�Ķ��ڽ��� �¿�� �����̰� �ϰ� �ʹ�
    public GameObject bluebox;
    public GameObject startpoint;
    public GameObject endpoint;
    public float speed = 1;
    public AnimationCurve curve;
    float currentTime;

    CharacterController bc;

    float rotspeed = 50f;
    float curvetime;


    //�Ǵ��� ���� ���ۿ� ���������� ���ڴ�.
    //�Ǵ��� ���������� �����ϸ� ���Ƽ� �ٽ� ���������� ������ ���ڴ�.
    void Start()
    {
        bc = GetComponent<CharacterController>();
        state = State.walk;
        ani.SetTrigger("walk");
    }

    // Update is called once per frame
    void Update()
    {
        //���� �Ǵ��� ���������� �����ϸ�
        //���Ƽ� �ٽ� ���������� ������ ���ڴ�
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