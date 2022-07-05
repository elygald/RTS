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
    
    public bool targetfixed= false;
    public float timedano;
    public float timer;
    public Vector3 target;
    public GameObject enemy;
    public GameObject projectile;
    public Transform projectileposition;
    private Color original;
    public Color colorselect;
     public float timedanoplanet;
    public float timerplanet;
    public float danoplanet;

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

        if (enemy != null)
        {
            target = enemy.transform.position;
   
            if (!move)
            {
                timer += Time.deltaTime;
                if (timer > timedano)
                {
                    timer = 0;
                
                    GameObject clone;
                    clone =  Instantiate(projectile, projectileposition);
                    clone.GetComponent<Projectile>().enemy = enemy.tag;
                    clone.transform.parent = null;
                }

                Vector3 direction = target - transform.position;
                Debug.DrawRay(transform.position, direction, Color.green);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

                Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Lerp(transform.rotation, angleAxis, Time.deltaTime * 50f);
            }
        }
        else {
            targetfixed = false;
        }

        lifebar.transform.localScale = new Vector3((lifeporcent * lifenow) * 2f, lifebar.transform.localScale.y, 0);
        //lifebar.GetComponent<LifeBar>().active = true;

        lifebar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        if (lifenow <= 0){
            Destroy(lifebar);
            Destroy(this.gameObject);
           
        }

        if (selected)
        {
            this.GetComponent<SpriteRenderer>().color = colorselect;
        }
        else {
            this.GetComponent<SpriteRenderer>().color = original;
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

        }

    }
     private void OnTriggerStay2D(Collider2D other){
         if(other.gameObject.tag == "planet"){
             if(other.gameObject.transform.GetComponent<Planet>().owner != this.gameObject.tag){

                timerplanet += Time.deltaTime;
                if (timerplanet > timedanoplanet)
                {
                    timerplanet = 0;
                    other.gameObject.transform.GetComponent<Planet>().life -= danoplanet;
                    other.gameObject.transform.GetComponent<Planet>().enemycolor =  original;
                    other.gameObject.transform.GetComponent<Planet>().enemy = this.gameObject.tag;
                }
            }
        }
     }
}
