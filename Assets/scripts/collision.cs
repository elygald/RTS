using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public string enemy;
    private float _distance = 0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == enemy && 
            other.gameObject.tag != "background" )
            {
                targets.Add(other.gameObject);
                    if (!this.transform.parent.GetComponent<Nave>().enemy)
                        this.transform.parent.GetComponent<Nave>().enemy = other.gameObject;
                        _distance = Vector2.Distance(this.transform.position, other.transform.position);
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
                }else if (!this.transform.parent.GetComponent<Nave>().enemy)
                {
                    targets.Clear();
                }
            }
    }
    void Update()
    {
        if (this.transform.parent.GetComponent<Nave>().enemy == null)
        {
            if(targets.Count != 0) { 
                foreach (var target in targets)
                {
                    if(target!= null) {
                        this.transform.parent.GetComponent<Nave>().enemy = _distance >= Vector2.Distance(this.transform.position, target.transform.position) ? target : this.transform.parent.GetComponent<Nave>().enemy;
                        _distance = Vector2.Distance(this.transform.position, target.transform.position);
                    }
                    else{
                        targets.Remove(target);
                    }
                }
            }

        }
       
    }

}
