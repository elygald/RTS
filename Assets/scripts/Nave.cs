using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    public bool selected = false;
    Rigidbody2D rb;
    Vector2 mousepos;
    float rotation = 200f;
    float speed = 2f;
   public bool move = false;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            this.GetComponent<SpriteRenderer>().color = Color.cyan;
            
            if (Input.GetMouseButtonDown(0))
            {
                selected = false;
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                move = true;
            }
            if (move) { 
                Vector2 pointarget = (Vector2)transform.position - (Vector2)mousepos;

                pointarget.Normalize();

                float value = Vector3.Cross(pointarget, transform.up).z;

                rb.angularVelocity = rotation * value;

                rb.velocity = transform.up * speed;
                Debug.Log(Vector3.Distance(mousepos, transform.position));

                if (Vector3.Distance(mousepos, transform.position) < 0.5)
                {
                    move = false;
                    rb.velocity = transform.up * 0f;
                    rb.angularVelocity = rotation * 0f;
                }
               


            }
        }
    }
}
