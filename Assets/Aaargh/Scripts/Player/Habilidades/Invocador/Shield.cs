using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float speed = 15f;
    public LayerMask collisionMask;

    Vector3 m_dir;
    Rigidbody m_rigid;

    public int vida = 50;


    //Shield caso não de certo.
    //void Update()
    //{
    //Ray ray = new Ray(transform.position, transform.forward);
    //RaycastHit hit;

    //if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, collisionMask))
    //{
    //Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
    //float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
    //transform.eulerAngles = new Vector3(0, rot, 0);

    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Vector3 _wallNormal = collision.contacts[0].normal;
            m_dir = Vector3.Reflect(m_rigid.velocity, _wallNormal).normalized;
            m_rigid.velocity = m_dir * speed;
            Destroy(this.gameObject);
        }
        if(collision.gameObject.name == "bullet")
        {
            vida -= 10;
            if(vida <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
