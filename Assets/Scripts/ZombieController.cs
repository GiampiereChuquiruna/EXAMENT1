using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float vCorrer = 2, velocity = 4, cont = 0;
    
    Rigidbody2D rb;
    Collider2D cl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cont >= 0 ){
           rb.velocity = new Vector2(vCorrer, rb.velocity.y);
           cont = 5;
        }
        else if(cont <= 5) {
            rb.velocity = new Vector2(-vCorrer, rb.velocity.y);
            cont = 0;
        }
        
        

    }
}
