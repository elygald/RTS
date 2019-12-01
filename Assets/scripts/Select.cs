using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    Vector3 inicial;
    Vector3 final;
    Vector3 inicialr;
    Vector3 finalr;
    LineRenderer lineRenderer;
    EdgeCollider2D collider;
    bool mousepress = false;
    void Start()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        collider = this.gameObject.GetComponent<EdgeCollider2D>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inicial = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            mousepress = true;
        }
        if (mousepress) {
            final = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            inicialr = new Vector3(inicial.x, final.y);
            finalr = new Vector3(final.x, inicial.y);
            lineRenderer.SetPosition(0, new Vector3(finalr.x, finalr.y));
            lineRenderer.SetPosition(1, new Vector3(final.x, final.y));
            lineRenderer.SetPosition(2, new Vector3(inicialr.x, inicialr.y));
            lineRenderer.SetPosition(3, new Vector3(inicial.x, inicial.y));

            Vector2[] colliderpoints;
            colliderpoints = collider.points;
            colliderpoints[0] = new Vector3(finalr.x, finalr.y);
            colliderpoints[1] = new Vector3(final.x, final.y);
            colliderpoints[2] = new Vector3(inicialr.x, inicialr.y);
            colliderpoints[3] = new Vector3(inicial.x, inicial.y);
            colliderpoints[4] = new Vector3(finalr.x, inicial.y);
            collider.points = colliderpoints;
            // collider.offset = lineRenderer.bounds.center;
        }
        if (Input.GetMouseButtonUp(0))
        {
            final = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //lineRenderer.SetPosition(1, new Vector3(final.x, final.y));
            mousepress=false;
        }

        lineRenderer.enabled = mousepress;
        // Debug.Log("position ini:" + inicial + " final" + final);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("nave")){
            collision.GetComponent<Nave>().selected = true;  
        }
    }
}
