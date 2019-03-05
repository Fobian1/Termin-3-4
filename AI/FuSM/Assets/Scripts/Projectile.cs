using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 target;
    public float speed, distance = 0.25f;
    public GameObject rapidEnemy;
    public Transform enemy;

    void Start() {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        target = new Vector2(enemy.position.x, enemy.position.y);    
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);   

        if(Vector2.Distance(transform.position, target) < distance) {
            Instantiate(rapidEnemy, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
