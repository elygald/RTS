using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector2 inicial;
    Vector2 final;
    LineRenderer lineRenderer;
    
    void Start()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
               
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            lineRenderer.enabled = true;
            inicial = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //lineRenderer.SetPosition(0, new Vector3(inicial.x, inicial.y)  );
        } 
        if (Input.GetMouseButtonUp(0))
        {
            final = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //lineRenderer.SetPosition(1, new Vector3(final.x, final.y));
        }
        
        lineRenderer.SetPosition(1, new Vector3(final.x, final.y));
        Debug.Log("position ini:" + inicial + " final" + final);
        lineRenderer.enabled = false;
     
    }   
}
