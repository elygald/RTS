using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public string enemy;
    private float _distance = 0f;
    // indentificar o inimigo se tiver um inimigo selecionado atirar nele
    // se não tiver em nenhum inimigo selecionado atirar no mais proximo 
    // para atirar o inimigo tem que estar dentro da area de ação 
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == enemy && 
            other.gameObject.tag != "background" )
        {         
            Debug.Log(this.transform.parent.GetComponent<Nave>().enemy);
            if (this.transform.parent.GetComponent<Nave>().enemy == null)
            {
                this.transform.parent.GetComponent<Nave>().enemy = other.gameObject;          
            }
            else if(!this.transform.parent.GetComponent<Nave>().targetfixed)
            {
                _distance = Vector2.Distance(this.transform.position , other.transform.position);
                float dist = Vector2.Distance(this.transform.position, this.transform.parent.GetComponent<Nave>().enemy.transform.position);
                this.transform.parent.GetComponent<Nave>().enemy = _distance < dist ? other.gameObject : this.transform.parent.GetComponent<Nave>().enemy;
                          
            }
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
         if(other.gameObject.tag == enemy && 
            other.gameObject.tag != "background" )
        {    
            if(this.transform.parent.GetComponent<Nave>().targetfixed && this.transform.parent.GetComponent<Nave>().enemy != null){
                if(GameObject.ReferenceEquals(other.gameObject, this.transform.parent.GetComponent<Nave>().enemy.gameObject))
                    this.transform.parent.GetComponent<Nave>().enemy = null;
            }else{
                this.transform.parent.GetComponent<Nave>().enemy = null;
            }
            
        }
    }

}
