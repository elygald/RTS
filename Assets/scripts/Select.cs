using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Select : MonoBehaviour
{
    public Camera Cam;
    RaycastHit R;

    GameObject[] UnidadesSel;

    float MediaTamUnidades;
    float right;
    Vector3 DestinoTropas;

    GameObject[] UnidadesSelAux ;
    int QndUnidades;

    public Image ImagemSelecao;
    Vector3 PosMinMouse;
    Vector3 PosMaxMouse;
    Vector3 PosIniMouse;
//-------------------------------------------
    void Start () {
        UnidadesSel = GameObject.FindGameObjectsWithTag("nave");
        MediaTamUnidades = 0.7f;
        right = 0.7f;
    }

    void Update()
    {
        int i = 0;

        if(Input.GetMouseButtonDown(0))
        {
            PosMinMouse = Input.mousePosition; 
            PosIniMouse = Input.mousePosition;
        }


        if(Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x >= PosIniMouse.x)
            {
                PosMaxMouse.x = Input.mousePosition.x;   
            
            }
        else
        {
            PosMaxMouse.x = PosIniMouse.x;
            PosMinMouse.x = Input.mousePosition.x; 
        }
        
        
        if (Input.mousePosition.y >= PosIniMouse.y)
        {
            PosMaxMouse.y = Input.mousePosition.y;    
        }
        else
        {
            PosMaxMouse.y = PosIniMouse.y;
            PosMinMouse.y = Input.mousePosition.y;
        }
        
        ImagemSelecao.rectTransform.anchorMax = new Vector2(PosMaxMouse.x / Screen.width,PosMaxMouse.y / Screen.height);
        ImagemSelecao.rectTransform.anchorMin = new Vector2(PosMinMouse.x / Screen.width,PosMinMouse.y / Screen.height);  
        
        }

        if(Input.GetMouseButtonUp(0))
        {
            
            RaycastHit2D PosMinMouse2d = Physics2D.Raycast(Cam.ScreenToWorldPoint(PosMinMouse), Vector2.zero);
            RaycastHit2D PosMaxMouse2d  = Physics2D.Raycast(Cam.ScreenToWorldPoint(PosMaxMouse), Vector2.zero);
            List<GameObject> items = new List<GameObject> (GameObject.FindGameObjectsWithTag("nave"));
            items.AddRange (new List<GameObject> (GameObject.FindGameObjectsWithTag ("enemy")));
            UnidadesSel = items.ToArray();
 
            UnidadesSelAux = new GameObject[UnidadesSel.Length];
            QndUnidades = 0;
        
        
        for(i = 0; i<UnidadesSel.Length; i++)
        {
            if(UnidadesSel[i].transform.position.x > PosMinMouse2d.point.x &&
                UnidadesSel[i].transform.position.x < PosMaxMouse2d.point.x &&
                UnidadesSel[i].transform.position.y > PosMinMouse2d.point.y &&
                UnidadesSel[i].transform.position.y < PosMaxMouse2d.point.y )
            {
                UnidadesSelAux[QndUnidades] = UnidadesSel[i];
                UnidadesSel[i].GetComponent<Nave>().selected = true;
                QndUnidades++;
            }
        }
        
        UnidadesSel = new GameObject[QndUnidades];
        for(i = 0; i<QndUnidades; i++)
        {
            UnidadesSel[i] = UnidadesSelAux[i];
        }
        
        ImagemSelecao.rectTransform.anchorMax = new Vector2(0,0);
        ImagemSelecao.rectTransform.anchorMin = new Vector2(0,0);
        }
    
        i = 0;
        if(Input.GetMouseButtonDown(1))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) 
            {
                DestinoTropas = hit.point;
                float lado = Mathf.Floor(Mathf.Sqrt(UnidadesSel.Length));
                float first = 0;
                Vector3 DestinoUnidade;
                foreach (GameObject Unidade in UnidadesSel)
                {        
        
                    DestinoUnidade = DestinoTropas;
                  
                    float x;
                    float y;
                    x = (i/lado);
                    y = (i%lado);

                    if (i % lado == 0)
                        first = x;
                    else
                        x = first;

                    DestinoUnidade -= ((lado/2) - x)*right*Vector3.right;
                    DestinoUnidade -= ((lado/2) - y)*MediaTamUnidades*Vector3.up;
                    Debug.Log("first " + first);
                    Unidade.GetComponent<Nave>().selected = true;
                    Unidade.GetComponent<Nave>().move = true;
                    Unidade.GetComponent<Nave>().DestinoUnidade = DestinoUnidade;
                    i++;
                }
                
            }

        }
    }

   
}
