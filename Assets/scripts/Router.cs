using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    bool active = false;
    Vector2 mousepos;
    LineRenderer lineRenderer;
    public GameObject nave;
    void Start()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector3(0f, 0f));
        lineRenderer.SetPosition(1, new Vector3(0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(nave.GetComponent<Nave>().move){
            mousepos = nave.GetComponent<Nave>().DestinoUnidade;
            active=true;
        }     

        if(active){   
            lineRenderer.SetPosition(1, new Vector3(nave.transform.position.x, nave.transform.position.y));
            lineRenderer.SetPosition(0, new Vector3(mousepos.x, mousepos.y));
        }
    }
}
