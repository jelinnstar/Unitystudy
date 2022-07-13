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
        // 사용자의 입력에 따라 방향 전환을 한다.
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
        // 점프 구현
        yVelocity += gravity * Time.deltaTime;

        // 땅에 있을 때 점프한다.
        if (cc.isGrounded)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        // 만약 Freeze상태라면
        if (isFreeze)
        {
            // 시간이 흐르다가
            currentTime += Time.deltaTime;
            // 만약 현재시간이 Freeze시간을 초과하면
            if (currentTime > freezeTime)
            {
                // 체력을 만땅채우고싶다.

                php.HP = PlayerHP.instance.maxHP;
                // Freeze상태를 끝내고싶다. 
                isFreeze = false;
                ani.SetTrigger("Run");
            }
            // 함수를 바로 종료하고싶다.
        }


        //점프 개수가 최대점프개수보다 적고 점프키를 눌렀다면
        if (false == isFreeze && jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            jumpCount++;
            yVelocity = jumpPower;

            // 점프하는 애니메이션 작동한다.
            ani.SetTrigger("Jumping");
            theAudioManager.PlaySFX("Jump");

            // 점프를 1번할 때마다 체력이 0.1 감소하고 싶다.
            php.HP = php.HP - 0.1f;
            // ★ 만약 플레이어의 체력이 0이하라면 FreezeTime동안 숨을 헐떡거린다. ★
            if (php.HP <= 0)
            {
                state = State.InjuredIdle;
                ani.SetTrigger("InjuredIdle");
                theAudioManager.PlaySFX("InjuredIdle");

                isFreeze = true;
                currentTime = 0;
            }
        }

        //사용자의 입력에 따라 상하좌우로 움직인다.
        float h = 0;
        float v = 0;
        if (false == isFreeze)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }

        ani.SetFloat("h", h);
        ani.SetFloat("v", v);

        // 상하좌우로 방향을 만들고
        Vector3 dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir);
        // dir의 크기를 1로 만들어야 한다
        dir.y = 0;

        dir.Normalize();

        // 키입력이 있고 방향이 존재할때
        if (Input.anyKey && (h != 0 || v != 0))
        {
            // 몸의 앞방향을 이동방향과 일치시키고싶다.
            body.forward = dir;
        }

        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        // 그 방향으로 이동하고 싶다.
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


    // 감지충돌
    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        // 만약 부딪힌 녀석의 이름에 carpet가 포함되어있다면
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
        // 그렇지않고 부딪힌 녀석의 이름에 tramperin이 포함되어 있다면
        else if (other.gameObject.name.Contains("tramperlin"))
        {
            // 위로 이동하고싶다
            yVelocity = jumpPower * 1.8f;
            // 점프행위를 막고싶다.
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
            //내리고 싶다.
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



