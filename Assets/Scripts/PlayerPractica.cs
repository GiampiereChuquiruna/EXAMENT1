using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPractica : MonoBehaviour
{
    public float velocity = 4, jumpForce = 8, vCorrer = 6;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;


    const int QUIETO = 0;
    const int CAMINAR = 1;
    const int ATACAR = 2;
    const int CORRER = 3;
    const int SALTAR = 4;    
    bool puedeSaltar = true;

    Vector3 lastCheckpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Iniciando");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {  
        
       if(Input.GetKey(KeyCode.RightArrow)){
           sr.flipX = false;
           if(Input.GetKey("x")){
            rb.velocity = new Vector2(vCorrer, rb.velocity.y);
            ChangeAnimation(CORRER);
           }
           else {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            ChangeAnimation(CAMINAR);
           }          
        }

        else if(Input.GetKey(KeyCode.LeftArrow)){
           sr.flipX = true;
           if(Input.GetKey("x")){
            rb.velocity = new Vector2(-vCorrer, rb.velocity.y);
            ChangeAnimation(CORRER);
           }
           else {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            ChangeAnimation(CAMINAR);
           }  
        }

        else if(Input.GetKey("z")){
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ATACAR);
           }
        
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(QUIETO);
        }
        if(Input.GetKey(KeyCode.Space) && puedeSaltar==true){
            //rb.velocity = new Vector2(rb.velocity.x, velSalto);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            ChangeAnimation(SALTAR);
            puedeSaltar = false;
        }
    }

   void OnCollisionEnter2D(Collision2D other){
        puedeSaltar = true;
        if(other.gameObject.tag == "Enemy"){
            Debug.Log("Estas muerto");
        }
        if(other.gameObject.name =="DarkHole")//para colisionar con el piso de fondo
        {
            if(lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
        } 
    }   
   
   void OnTriggerEnter2D(Collider2D other)//para reconocer el checkponit(transparente)
    {
        Debug.Log("Trigger");//aplicar la pocion isTrigger en la configuracion
        lastCheckpointPosition = transform.position;
    }

   private void ChangeAnimation (int v){
        animator.SetInteger("Estado", v);
    }
}
