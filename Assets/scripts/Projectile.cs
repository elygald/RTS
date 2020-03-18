using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D m_Rigidbody;
    public float m_Speed;
    public string enemy;
    public float timelive;
    float timer = 0f;
    public float valuedano;

    void Update()
    {
        m_Rigidbody.velocity = transform.up * m_Speed;
        timer += Time.deltaTime;
        if (timer > timelive)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == enemy)
        {
            other.GetComponent<Nave>().lifenow -= valuedano;
            Destroy(gameObject);
        }
    }
}
