using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
   public GameObject dad;
   private void OnTriggerStay2D(Collider2D other) {
        if(dad.gameObject.tag != this.gameObject.tag){
            if(other.gameObject.tag == "nave" && dad.gameObject.tag != "nave")
            {
                other.GetComponent<Nave>().dano = true;
                other.GetComponent<Nave>().target = this.transform.position;
            }
             if(other.gameObject.tag == "enemy" && dad.gameObject.tag != "enemy")
            {
                other.GetComponent<Nave>().dano = true;
                other.GetComponent<Nave>().target = this.transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
            if((other.gameObject.tag == "nave") || (other.gameObject.tag == "enemy"))    
               other.GetComponent<Nave>().dano = false;

    }
    

}
