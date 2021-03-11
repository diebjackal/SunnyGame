using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;

    private int jumpPower = 5;
    private int maxSpeed = 5;

    public bool isJump;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        //기본 움직이기
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        Speed();
        Animations(h);
    }

    //속도
    void Speed()
    {
        //멈추는 속도
        if (rigid.velocity.x > maxSpeed) // right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //최대 속도
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);

    }

    void Animations(float h)
    {
        //애니메이션
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isRun", false);
        else
            anim.SetBool("isRun", true);

        //보는 방향
        if (h > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (h < 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            anim.SetBool("isJump", true);
            isJump = true;
        }
    }
}