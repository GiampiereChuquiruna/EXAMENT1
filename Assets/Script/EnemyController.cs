using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocity = 2;
    int vida = 2;
    Rigidbody2D rb;
    SpriteRenderer sr;
    private GameManagerController gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();        
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    { 
        if(vida < 1){
            Destroy(this.gameObject);
        }
        rb.velocity = new Vector2(0, rb.velocity.y);
    }   
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Kunai")
        {
            vida = vida - 1;
        }
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            gameManager.GanarPuntos(1);
            vida = vida - 2;
        }
    } 
}
