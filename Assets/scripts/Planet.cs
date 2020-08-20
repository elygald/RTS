using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float life = 100f;
    public string owner = "";
    public Color enemycolor;

    public string enemy= "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life<=0f){
            this.GetComponent<SpriteRenderer>().color = enemycolor;
            owner = enemy;
            life = 100f;
        }
    }
}
