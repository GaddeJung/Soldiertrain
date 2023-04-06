using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator anime;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    // »ç¿îµå
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip attackSound;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    void Think()
    {
        nextMove = Random.Range(-2, 3);

        if (nextMove == -2 || nextMove == 2)
        {
            anime.SetBool("Run", true);
            spriteRenderer.flipX = nextMove == -2;
            CharSound.instance.CharSoundPlay("Run", runSound);
            Invoke("StopMove", 3);
            InvokeRepeating("PlayCharSound", 0, runSound.length);
        }
        else if (nextMove == -1 || nextMove == 1)
        {
            anime.SetBool("Walk", true);
            spriteRenderer.flipX = nextMove == -1;
            CharSound.instance.CharSoundPlay("walk", walkSound);
            Invoke("StopMove", 3);
            InvokeRepeating("PlayCharSound", walkSound.length + 0.5f, 0.5f);
        }
        else if (nextMove == 0)
        {
            if (Random.value < 0.1f && !anime.GetBool("Attack"))
            {
                anime.SetBool("Attack", true);
                CharSound.instance.CharSoundPlay("Attack", attackSound);
                Invoke("StopAttack", 1.5f);
                InvokeRepeating("PlayCharSound", 0, attackSound.length);
            }

            Invoke("Think", 3);
        }
    }

    void StopMove()
    {
        nextMove = 0;

        if (anime.GetBool("Run"))
        {
            anime.SetBool("Run", false);
        }
        else if (anime.GetBool("Walk"))
        {
            anime.SetBool("Walk", false);
        }

        CancelInvoke("PlayCharSound");

        Invoke("Think", 3);
    }

    void StopAttack()
    {
        anime.SetBool("Attack", false);

        Invoke("Think", 3);
    }

    private void PlayCharSound()
    {
        if (anime.GetBool("Run"))
        {
            CharSound.instance.CharSoundPlay("Run", runSound);
        }
        else if (anime.GetBool("Walk"))
        {
            CharSound.instance.CharSoundPlay("Walk", walkSound);
        }
        else if (anime.GetBool("Attack"))
        {
            CharSound.instance.CharSoundPlay("Attack", attackSound);
        }

    }

    void Start()
    {
        if (nextMove != 0)
        {
            InvokeRepeating("PlayCharSound", 0, walkSound.length);
        }
    }
}
