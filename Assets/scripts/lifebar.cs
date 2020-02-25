using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifebar : MonoBehaviour
{
    public float timelife;
    public float timer;
    public bool active = true;
    //public float life;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = active;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localScale = new Vector3((life), this.transform.localScale.y, 0);
       
        if(active){
            this.GetComponent<SpriteRenderer>().enabled = active;
            timer += Time.deltaTime;
            if (timer > timelife)
            {
                timer = 0;
                //active =false;
                this.GetComponent<SpriteRenderer>().enabled = active;
                
            }

        }
    }
}
