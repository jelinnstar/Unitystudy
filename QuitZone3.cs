using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitZone3 : MonoBehaviour
{
    public Transform FourthPosition;
    public Transform carpetEnded;
    public CarperMove[] carpets;
    public GameObject Carpets;
    Vector3 dir;


    //���� �÷��̾�� ������
    private void OnTriggerEnter(Collider other)
    {
        // ���� �÷��̾�� �ε����ٸ�..
        if (other.name.Contains("player"))
        {
            print("QuitZone3 :: OnTriggerEnter : " + other.name);
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.transform.position = FourthPosition.position;
            other.transform.rotation = FourthPosition.rotation;
            player p = other.gameObject.GetComponent<player>();
            p.MakeTeleportVFX();
            other.gameObject.GetComponent<CharacterController>().enabled = true;
            Invoke(nameof(ResetCarpet), 2); // 2�ʵ� DelayCarpet ȣ��
        }

    }

    // ī��� ��ġ�� �ʱ�� �ű��
    void ResetCarpet()
    {
        print("ResetCarpet");
        for (int i = 0; i < carpets.Length; i++)
        {
            //�� �� �Ŀ�

            //ī����� ó�� ��ġ�� �����϶�� �ϰ� �ʹ�.
            carpets[i].transform.position = carpets[i].origin;
            // ī���� �� �����̰� ����
            carpets[i].isPlaying = false;
        }
    }

    void Start()
    {
    }

}
