using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter2 : MonoBehaviour
{
    public LayerMask collisionMask;

    public float speed;
    private Transform player;
    private Vector3 target;

    //Bala do inimigo
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            DestroyProjectile();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
            player.GetComponent<Player2>().vida -= 10;
        }
        if (other.gameObject.name== "Shield")
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
