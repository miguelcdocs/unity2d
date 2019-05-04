using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float visionRadius;
    public float attackRadius;
    public float speed;
    GameObject player;
    Vector3 initialPosition;
    Animator anim;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = initialPosition;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadius,
            1 << LayerMask.NameToLayer("Default"));

        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        if(hit.collider != null){

            if(hit.collider.tag == "Player"){
                target = player.transform.position;
            }
        }
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position);
        if(target != initialPosition && distance < attackRadius){
           // atacariamos 
        }
        else {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
            //anim de movimiento
        }

        if(target == initialPosition && distance < 0.02f){
            transform.position = initialPosition;
            // animacion a parado
        }
        Debug.DrawLine(transform.position, target, Color.green);

    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
