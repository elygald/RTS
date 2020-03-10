using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public GameObject target;
    private void OnTriggerStay2D(Collider2D other)
    {

        if (this.gameObject.tag != other.gameObject.tag && other.gameObject.tag != "background")
        {
            target = other.gameObject;
                if (!this.GetComponent<Nave>().enemy)
                    this.GetComponent<Nave>().enemy = other.gameObject;
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (this.gameObject.tag != other.gameObject.tag && other.gameObject.tag != "background")
        {
            if (this.GetComponent<Nave>().enemy) { 
                this.GetComponent<Nave>().enemy = null;
                target = null;
            }
        }
    }
    void Update()
    {
        if ((!this.GetComponent<Nave>().enemy)&&(target)) {
            this.GetComponent<Nave>().enemy = target;
        }
    }

}
