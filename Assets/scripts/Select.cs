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

    List<GameObject> itemsSelected;

    float MediaTamUnidades;
    float right;
    Vector3 DestinoTropas;

    GameObject[] UnidadesSelAux;
    int QndUnidades;

    public Image ImagemSelecao;
    Vector3 PosMinMouse;
    Vector3 PosMaxMouse;
    Vector3 PosIniMouse;
   
    void Start() {
        itemsSelected = new List<GameObject>();
        UnidadesSel = GameObject.FindGameObjectsWithTag("nave");
        MediaTamUnidades = 0.7f;
        right = 0.7f;
    }

    void Update()
    {
        this.Selected();
        this.SelectOneUnit();
        this.router();
    }

    private void SelectOneUnit() {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider is BoxCollider2D) { 
                if (hit.collider.gameObject.tag == "nave")
                {
                    hit.collider.gameObject.GetComponent<Nave>().selected = true;
                    itemsSelected.Add(hit.collider.gameObject);

                }
            }
        }
    }
    private void deselect() {
        foreach (var item in itemsSelected)
        {
            if(item != null)
            {
                item.GetComponent<Nave>().selected = false;
            }
        }

    }
    private void router() {
     
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D PosMinMouse2d = Physics2D.Raycast(Cam.ScreenToWorldPoint(PosMinMouse), Vector2.zero);
            RaycastHit2D PosMaxMouse2d = Physics2D.Raycast(Cam.ScreenToWorldPoint(PosMaxMouse), Vector2.zero);
            List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("nave"));
            items.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy")));
       
            foreach (var unit in items)
            {
                if (unit.transform.position.x > PosMinMouse2d.point.x &&
                   unit.transform.position.x < PosMaxMouse2d.point.x &&
                   unit.transform.position.y > PosMinMouse2d.point.y &&
                   unit.transform.position.y < PosMaxMouse2d.point.y)
                {
                    itemsSelected.Add(unit);
                    unit.GetComponent<Nave>().selected = true;
                }

            }

            ImagemSelecao.rectTransform.anchorMax = new Vector2(0, 0);
            ImagemSelecao.rectTransform.anchorMin = new Vector2(0, 0);
            PosMinMouse = new Vector3(0,0,0);
            PosIniMouse = new Vector3(0,0,0);
        }

        if (Input.GetMouseButtonDown(1))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                DestinoTropas = hit.point;
                float lado = Mathf.Round(Mathf.Sqrt(itemsSelected.Count));
                float first = 0;
                Vector3 DestinoUnidade;
                int i = 0;
                foreach (GameObject Unidade in itemsSelected)
                {
                    if (Unidade)
                    {
                        DestinoUnidade = DestinoTropas;

                        float x;
                        float y;
                        x = (i / lado);
                        y = (i % lado);

                        if (i % lado == 0)
                            first = x;
                        else
                            x = first;

                        DestinoUnidade -= ((lado / 2) - x) * right * Vector3.right;
                        DestinoUnidade -= ((lado / 2) - y) * MediaTamUnidades * Vector3.up;
                        Unidade.GetComponent<Nave>().selected = true;
                        Unidade.GetComponent<Nave>().move = true;
                        Unidade.GetComponent<Nave>().DestinoUnidade = DestinoUnidade;
                        if(hit.collider.gameObject.tag == "enemy"){
                            Unidade.GetComponent<Nave>().targetfixed = true;
                            Unidade.GetComponent<Nave>().enemy = hit.collider.gameObject;
                        }else{
                            Unidade.GetComponent<Nave>().targetfixed = false;
                        }
                        
                        i++;
                    }
                }

            }

        }
    }

    private void Selected()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            this.deselect();
            itemsSelected.Clear();
            PosMinMouse = Input.mousePosition;
            PosIniMouse = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
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

            ImagemSelecao.rectTransform.anchorMax = new Vector2(PosMaxMouse.x / Screen.width, PosMaxMouse.y / Screen.height);
            ImagemSelecao.rectTransform.anchorMin = new Vector2(PosMinMouse.x / Screen.width, PosMinMouse.y / Screen.height);

        }
    }

}
