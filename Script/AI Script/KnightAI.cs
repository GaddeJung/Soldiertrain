using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI : MonoBehaviour
{
    Animator anime;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    public AudioClip runSound;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
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
        nextMove = Random.Range(-1, 2);


        anime.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
        {
            CharSound.instance.CharSoundPlay("Run", runSound);
            spriteRenderer.flipX = nextMove == -1;
            Invoke("Stop", 3);
            InvokeRepeating("PlayCharSound", 0, runSound.length);
        }
        if (nextMove == 0)
        {
            Invoke("Think", 3);
        }
    }

    private void Stop()
    {
        nextMove = 0;

        CancelInvoke("PlayCharSound");
        Invoke("Think", 3);
        
        anime.SetInteger("WalkSpeed", nextMove);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (nextMove == 1)
            {
                nextMove = -1;
                if (nextMove != 0)
                    spriteRenderer.flipX = nextMove == -1;

            }

            else if (nextMove == -1)
            {
                nextMove = 1;
                if (nextMove != 0)
                    spriteRenderer.flipX = nextMove == -1;

            }
        }
    }
    private void PlayCharSound()
    {
        
        CharSound.instance.CharSoundPlay("Run", runSound);
        
    }

}
