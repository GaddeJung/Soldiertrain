using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCharacter : MonoBehaviour
{
    Animator anime;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    private void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        anime= GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 좌우 이동 3초 후에 이동하기
        Invoke("Think", 3);
    }

    private void FixedUpdate()
    {
        // 이동방식
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


    }

    void Think()
    {
        nextMove = Random.Range(-1,2);

        Invoke("Think", 3);

        anime.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
        spriteRenderer.flipX = nextMove == -1;
    }
}
