using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPractica : MonoBehaviour
{
    public float velocity = 4, jumpForce = 8, vCorrer = 2;

    public GameObject bullet;
    //public Text scoreText;
    private GameMangerController gameManager;
    public AudioClip jumClip;
    public AudioClip bulletClip;
    public AudioClip coin;


    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;



    //const int QUIETO = 0;
    //const int CAMINAR = 1;
    //const int ATACAR = 2;
    const int CORRER = 1;
    const int SALTAR = 2; 
    const int MORIR = 3;     
    int d = 1, cont = 2;  
    //bool puedeSaltar = true;
    
    Vector3 lastCheckpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("Iniciando");
        gameManager = FindObjectOfType<GameMangerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {  

       if(Input.GetKey(KeyCode.RightArrow)){
           sr.flipX = false;
           //if(Input.GetKey("x")){
            rb.velocity = new Vector2(vCorrer, rb.velocity.y);
            ChangeAnimation(CORRER);
          // }
           //else {
            //rb.velocity = new Vector2(velocity, rb.velocity.y);
           // ChangeAnimation(CAMINAR);
          // }   
           d = 1;       
        }

        else if(Input.GetKey(KeyCode.LeftArrow)){
           sr.flipX = true;
           //if(Input.GetKey("x")){
            rb.velocity = new Vector2(-vCorrer, rb.velocity.y);
            ChangeAnimation(CORRER);
           //}
           //else {
           // rb.velocity = new Vector2(-velocity, rb.velocity.y);
           // ChangeAnimation(CAMINAR);
          // }  
           d = -1;
        }
        else if(Input.GetKeyUp(KeyCode.K)){
            if (gameManager.lives > 0){
            var bulletPosition = transform.position + new Vector3(d,0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            //controller.SetLeftDirection();
            if(d>0)controller.SetRightDirection();
            else if(d<0)controller.SetLeftDirection();
            //controller.SetScoreText(scoreText);                 
            }
            

        }
        //else if(Input.GetKey("z")){
       //    rb.velocity = new Vector2(0, rb.velocity.y);
        //    ChangeAnimation(ATACAR);
        //   }
        
        //else{
        //    rb.velocity = new Vector2(0, rb.velocity.y);
        //    ChangeAnimation(CORRER);
       // }
        if(Input.GetKeyDown(KeyCode.Space) && cont > 0){
            //rb.velocity = new Vector2(rb.velocity.x, velSalto);
               rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
               ChangeAnimation(SALTAR);
               //puedeSaltar = false;
               cont--;
               audioSource.PlayOneShot(jumClip);
        }
    }

   void OnCollisionEnter2D(Collision2D other){
        //puedeSaltar = true;
        cont = 2;
        if(other.gameObject.tag == "Enemy"){
            Debug.Log("Estas muerto");
            
        }
        if(other.gameObject.name =="Zombie")
        {
            ChangeAnimation(MORIR); 
            if(lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;            
            }
        } 
        if(other.gameObject.tag == "monedaB"){
            audioSource.PlayOneShot(coin);
            Destroy(other.gameObject);
            gameManager.GanarBronce();
            
        }
        if(other.gameObject.tag == "monedaP"){
            audioSource.PlayOneShot(coin);
            Destroy(other.gameObject);
            gameManager.GanarPlata();
            
        }
        if(other.gameObject.tag == "monedaO"){
            audioSource.PlayOneShot(coin);
            Destroy(other.gameObject);
            gameManager.GanarOro();
            
        }

    }   
   
   void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        lastCheckpointPosition = transform.position;
        other.GetComponent<Collider2D>().enabled = false;
    }

   private void ChangeAnimation (int v){
        animator.SetInteger("Estado", v);
    }
}
