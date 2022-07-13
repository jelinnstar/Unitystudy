using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// �����ߴٰ� Abox�� ������ϸ� �̵��ϰ�ʹ�.
// �̵������ ũ�⸦ �̸� ���س���ʹ�.
// �÷��̾ ���� �ε����� ���� �ٲٰ�ʹ�.
// �÷��̾ ������ ���������� ��������ʹ�.
public class CarperMove : MonoBehaviour
{
    public bool isPlaying;
    public float speed = 1;
    public Vector3 velocity;
    Color bodyColor;
    public Vector3 origin;

    void Start()
    {
        origin = transform.position;
        bodyColor = GetComponentInChildren<MeshRenderer>().material.color;
        this.isPlaying = false;
        velocity = -transform.forward * speed;
    }

    public void ChangeColor()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.cyan;
    }

    public void RestoreColor()
    {
        GetComponentInChildren<MeshRenderer>().material.color = bodyColor;

    }

    void Update()
    {
        if (isPlaying)
        {
            transform.position += velocity * Time.deltaTime;
        }
    }

    // ����(ī��) �������� �ε����� ��
    private void OnTriggerEnter(Collider other)
    {
        //���� �÷��̾���
        if (other.name.Contains("player"))
        {
            // ī�� �̵� ����
            // ������ �ٲٰ�ʹ�.
            this.ChangeColor();
            AudioManager.instance.PlaySFX("CarpetSelect");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���� ������(�÷��̾�)�� �ε����� ���� ��
        if (other.name.Contains("player"))
        {
            // ī�� �̵� ��
            //isPlaying = false;
            this.RestoreColor();
        }
    }
}
