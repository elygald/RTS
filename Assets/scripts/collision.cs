using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public string enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == enemy && 
            other.gameObject.tag != "background" )
            {
                targets.Add(other.gameObject);
                    if (!this.transform.parent.GetComponent<Nave>().enemy)
                        this.transform.parent.GetComponent<Nave>().enemy = other.gameObject;
            }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == enemy && 
            other.gameObject.tag != "background")
            {
                if (this.transform.parent.GetComponent<Nave>().enemy) { 
                    this.transform.parent.GetComponent<Nave>().enemy = null;
                    targets.Remove(other.gameObject);
                }
            }
    }
    void Update()
    {
        if (this.transform.parent.GetComponent<Nave>().enemy == null)
        {
            foreach (var target in targets)
            {
                if(target!= null) {
                    this.transform.parent.GetComponent<Nave>().enemy = target;
                }else{
                    targets.Remove(target);
                }
            }
        }
       
    }

}
