using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Nave : MonoBehaviour
{
    public bool selected = false;
    Rigidbody2D rb;
    public Vector3 DestinoUnidade;
    public float rotation = 500f;
    public float speed = 3f;
    public float life;
    public float lifenow;
    public float lifeporcent;
    public bool move = false;
    public GameObject lifebarPerfab;
    public GameObject lifebar;
    public bool dano = false;
  
    public float timedano;
    public float timer;
    public Vector3 target;
    public GameObject enemy;
    public GameObject projectile;
    public Transform projectileposition;
    private Color original;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); 
        lifenow = life;
        lifeporcent = 100/life ;
        lifeporcent = lifeporcent / 100;
        lifebar =(GameObject) Instantiate(lifebarPerfab, new Vector3( transform.position.x, transform.position.y + 0.25f, transform.position.z),Quaternion.Euler(transform.position));
        //lifebar.transform.parent = this.transform;
        original = this.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

        if (enemy)
        {
            target = enemy.transform.position;


            timer += Time.deltaTime;
            if (timer > timedano)
            {
                timer = 0;
               
                GameObject clone;
                clone =  Instantiate(projectile, projectileposition);
                clone.GetComponent<Projectile>().enemy = enemy.tag;
                clone.transform.parent = null;
            }

            if (!move)
            {
                Vector3 direction = target - transform.position;
                Debug.DrawRay(transform.position, direction, Color.green);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

                Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Lerp(transform.rotation, angleAxis, Time.deltaTime * 50f);
            }
        }
        else {

        }

        lifebar.transform.localScale = new Vector3((lifeporcent * lifenow) * 2f, lifebar.transform.localScale.y, 0);
        lifebar.GetComponent<lifebar>().active = true;



        lifebar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        if (lifenow <= 0){
            Destroy(lifebar);
            Destroy(this.gameObject);
           
        }
        
        if (selected)
        {
            if(this.gameObject.tag == "nave"){
                this.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            if(this.gameObject.tag == "enemy"){
                this.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (Input.GetMouseButtonDown(0))
            {
                selected = false;
                this.GetComponent<SpriteRenderer>().color = original;
            }
           
            
        }

        if (move)
        {

            Vector2 pointarget = (Vector2)transform.position - (Vector2)DestinoUnidade;

            pointarget.Normalize();


            float value = Vector3.Cross(pointarget, transform.up).z;

            rb.angularVelocity = rotation * value;

            rb.velocity = transform.up * speed;


            if (Vector3.Distance(DestinoUnidade, transform.position) < 0.1)
            {
                move = false;
                rb.velocity = transform.up * 0f;
                rb.angularVelocity = rotation * 0f;
            }
            // Debug.Log(this.transform.localScale);
        }
        
          
        
      
    }
}
