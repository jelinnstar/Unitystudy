using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Jumping,
        ReactBackwards,
        InjuredIdle,
        Dancing,
    }
    public State state;
    public Animator ani;
    AudioManager theAudioManager;

    public float jumpPower = 3.5f;
    public float gravity = -8f;
    public float speed = 10;
    public float boosterSpeed = 20;

    Vector3 dir;
    float yVelocity;
    int jumpCount;
    public int maxJumpCount = 2;

    CharacterController cc;
    public Transform body;
    public Transform goalZone;

    bool isFreeze = false;
    float currentTime;
    float boosterTime = 1;
    public float freezeTime = 5;
    PlayerHP php;
    public float playerHPValue;
    AngerHP pahp;
    public GameObject teleportFactory;
    public GameObject boosterFactory;

    void Start()
    {
        theAudioManager = AudioManager.instance;
        cc = GetComponent<CharacterController>();
        php = this.gameObject.GetComponent<PlayerHP>();
        pahp = this.gameObject.GetComponent<AngerHP>();
        state = State.Idle;
    }


    void Update()
    {
        if (GameManager.instance.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (state == State.Dancing)
        {
            TestGoGoalZone();
        }
        else
        {
            UpdateRotate();
            UpdateMove();
        }
    }

    public float rotSpeed = 1;
    private void UpdateRotate()
    {
        // ������� �Է¿� ���� ���� ��ȯ�� �Ѵ�.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, rotSpeed * -150 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotSpeed * 150 * Time.deltaTime);
        }
    }


    void UpdateMove()
    {
        // ���� ����
        yVelocity += gravity * Time.deltaTime;

        // ���� ���� �� �����Ѵ�.
        if (cc.isGrounded)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        // ���� Freeze���¶��
        if (isFreeze)
        {
            // �ð��� �帣�ٰ�
            currentTime += Time.deltaTime;
            // ���� ����ð��� Freeze�ð��� �ʰ��ϸ�
            if (currentTime > freezeTime)
            {
                // ü���� ����ä���ʹ�.

                php.HP = PlayerHP.instance.maxHP;
                // Freeze���¸� ������ʹ�. 
                isFreeze = false;
                ani.SetTrigger("Run");
            }
            // �Լ��� �ٷ� �����ϰ�ʹ�.
        }


        //���� ������ �ִ������������� ���� ����Ű�� �����ٸ�
        if (false == isFreeze && jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            jumpCount++;
            yVelocity = jumpPower;

            // �����ϴ� �ִϸ��̼� �۵��Ѵ�.
            ani.SetTrigger("Jumping");
            theAudioManager.PlaySFX("Jump");

            // ������ 1���� ������ ü���� 0.1 �����ϰ� �ʹ�.
            php.HP = php.HP - 0.1f;
            // �� ���� �÷��̾��� ü���� 0���϶�� FreezeTime���� ���� �涱�Ÿ���. ��
            if (php.HP <= 0)
            {
                state = State.InjuredIdle;
                ani.SetTrigger("InjuredIdle");
                theAudioManager.PlaySFX("InjuredIdle");

                isFreeze = true;
                currentTime = 0;
            }
        }

        //������� �Է¿� ���� �����¿�� �����δ�.
        float h = 0;
        float v = 0;
        if (false == isFreeze)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }

        ani.SetFloat("h", h);
        ani.SetFloat("v", v);

        // �����¿�� ������ �����
        Vector3 dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir);
        // dir�� ũ�⸦ 1�� ������ �Ѵ�
        dir.y = 0;

        dir.Normalize();

        // Ű�Է��� �ְ� ������ �����Ҷ�
        if (Input.anyKey && (h != 0 || v != 0))
        {
            // ���� �չ����� �̵������ ��ġ��Ű��ʹ�.
            body.forward = dir;
        }

        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        // �� �������� �̵��ϰ� �ʹ�.
        //cc.Move(dir * speed * Time.deltaTime);

        if (CARPET != null)
        {
            cc.Move((CARPET.velocity + velocity) * Time.deltaTime);
        }
        else
        {
            cc.Move(velocity * Time.deltaTime);
        }

        if (pahp.ANGRYHP >= pahp.maxANGRYHP)
        {
            boosterVFX();
            cc.Move(dir * boosterSpeed * Time.deltaTime);

            currentTime += Time.deltaTime;
            if (currentTime > boosterTime)
            {
                currentTime = 0;
                pahp.ANGRYHP = 0;
                cc.Move(dir * speed * Time.deltaTime);
                state = State.Run;
                ani.SetTrigger("Run");
            }
        }
        Cheat();
        TestGoGoalZone();
    }

    CarperMove CARPET;

    public void boosterVFX(float time = 0.3f)
    {
        GameObject bst = Instantiate(boosterFactory);
        bst.transform.position = transform.position;
        theAudioManager.PlaySFX("Booster");
    }


    public Transform aBox;
    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            cc.enabled = false;
            transform.position = aBox.position + Vector3.up * 2;
            cc.enabled = true;
        }
    }


    private void TestGoGoalZone()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            cc.enabled = false;
            transform.position = goalZone.position + Vector3.up * 3;
            cc.enabled = true;
            state = State.Dancing;
            ani.SetTrigger("Dancing");
            theAudioManager.PlaySFX("GoalVoice");
            theAudioManager.PlayGoalBGM();
        }
    }


    // �����浹
    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        // ���� �ε��� �༮�� �̸��� carpet�� ���ԵǾ��ִٸ�
        if (other.gameObject.name.Contains("Debris"))
        {
            pahp.ANGRYHP = pahp.ANGRYHP + 0.1f;
            state = State.ReactBackwards;
            ani.SetTrigger("ReactBackwards");
            CameraShake.instance.Shake(0.5f, 0.2f);
            theAudioManager.PlaySFX("Damage");
        }
        else if (other.gameObject.name.Contains("DebrisWall"))
        {
            pahp.ANGRYHP = pahp.ANGRYHP + 0.1f;
            state = State.ReactBackwards;
            ani.SetTrigger("ReactBackwards");
            CameraShake.instance.Shake(0.5f, 0.2f);
            theAudioManager.PlaySFX("Damage");
        }

        else if (other.gameObject.name.Contains("carpet"))
        {
            CarperMove cm = other.attachedRigidbody.gameObject.GetComponent<CarperMove>();
            if (cm != null)
            {
                cm.isPlaying = true;
                theAudioManager.PlaySFX("CarpetMove");
                if (CARPET == null)
                {
                    CARPET = cm;
                }
            }
        }
        // �׷����ʰ� �ε��� �༮�� �̸��� tramperin�� ���ԵǾ� �ִٸ�
        else if (other.gameObject.name.Contains("tramperlin"))
        {
            // ���� �̵��ϰ�ʹ�
            yVelocity = jumpPower * 1.8f;
            // ���������� ����ʹ�.
            jumpCount = maxJumpCount;
            theAudioManager.PlaySFX("TramperlinJump");

        }
        else if (other.gameObject.name.Contains("GoalZone"))
        {
            state = State.Dancing;
            ani.SetTrigger("Dancing");
            theAudioManager.PlaySFX("GoalVoice");
            theAudioManager.PlayGoalBGM();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("geine"))
        {
            pahp.ANGRYHP = pahp.ANGRYHP + 0.1f;
            state = State.ReactBackwards;
            ani.SetTrigger("ReactBackwards");
            CameraShake.instance.Shake(0.5f, 0.2f);
            theAudioManager.PlaySFX("Damage");
        }
        else if (collision.gameObject.name.Contains("bluebox"))
        {
            pahp.ANGRYHP = pahp.ANGRYHP + 0.1f;
            state = State.ReactBackwards;
            ani.SetTrigger("ReactBackwards");
            CameraShake.instance.Shake(0.5f, 0.2f);
            theAudioManager.PlaySFX("Damage");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit");

        if (CARPET != null && CARPET == other.attachedRigidbody.gameObject.GetComponent<CarperMove>())
        {
            //������ �ʹ�.
            CARPET = null;
        }
    }

    public void MakeTeleportVFX()
    {
        GameObject tlp = Instantiate(teleportFactory);
        tlp.transform.position = transform.position;
        theAudioManager.PlaySFX("Teleport");
    }
}



