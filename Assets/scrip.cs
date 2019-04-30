using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    public float speed = 4f;
    Rigidbody2D rb2d;
    Vector2 mov;

    Animator anim;

    CircleCollider2D attackCollider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        attackCollider = transform.GetChild(1).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if(mov != Vector2.zero){
            anim.SetFloat("movx", mov.x);
            anim.SetFloat("movy", mov.y);
            anim.SetBool("walking", true);
        } else {
              anim.SetBool("walking", false);
        }
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Player_attack");
        if(Input.GetKeyDown("space") && !attacking){
            anim.SetTrigger("attacking");
        }
       
        if( mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x/2, mov.y/2);
        if(attacking){
            float playbackTime = stateInfo.normalizedTime;
            if(playbackTime > 0.33 && playbackTime < 0.66) attackCollider.enabled = true;
            else attackCollider.enabled = false;
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);   
    }
}
