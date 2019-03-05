using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public enum AttackStates {
        attackBase,
        attackPlayer,
        evade
    }

    public float speed, health = 2, baseDistance, distanceSpeed, projectileDistance = 1000;
    private Transform playerPos;
    private Transform theBase;
    private Transform projectile;
    private Player player;

    public AttackStates CurrentES;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        theBase = GameObject.FindGameObjectWithTag("Base").transform;
        //projectile = GameObject.FindGameObjectWithTag("Projectile").transform;

        baseDistance = Vector2.Distance(transform.position, theBase.position);
        //projectileDistance = Vector2.Distance(transform.position, projectile.position);
    }

    void Update() {
        //print("projectile Distance : " + projectileDistance);
        baseDistance = Vector2.Distance(transform.position, theBase.position);
        //projectileDistance = Vector2.Distance(transform.position, projectile.position);
        if (baseDistance > 500) {
            distanceSpeed = 1;
        } else if (baseDistance <= 500 && baseDistance > 300) {
            distanceSpeed = 1.5f;
        } else if (baseDistance <= 300 && baseDistance > 150) {
            distanceSpeed = 2f;
        } else if (baseDistance <= 150) {
            distanceSpeed = 2.5f;
        }

        switch (CurrentES) {            
            case AttackStates.attackBase:
                //if (projectileDistance < 200) {
                //    CurrentES = AttackStates.evade;
                //}
                if (health > 1 || player.points >= 20) {
                    AttackBase();
                } else {
                    CurrentES = AttackStates.attackPlayer;
                }
                
                break;
            case AttackStates.attackPlayer:
                //if (projectileDistance < 200) {
                //    CurrentES = AttackStates.evade;
                //}
                if (health > 1 || player.points >= 20) {
                    CurrentES = AttackStates.attackBase;
                } else {
                    AttackPlayer();
                }
                break;
            //case AttackStates.evade:
            //    Evade();
            //    if (projectileDistance > 200 && health >= 1 || player.points >= 20) {
            //        CurrentES = AttackStates.attackBase;
            //    } else if (player.points < 20) {
            //        AttackPlayer();
            //    }
            //    break;
        }

        //if (player.health <= 2 || player.points >= 20) {
        //    transform.position = Vector2.MoveTowards(transform.position, theBase.position, speed * Time.deltaTime * endSpeed);
        //} else if (player.health <= 5 || player.points >= 10) {
        //    transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        //} else {
        //    transform.position = Vector2.MoveTowards(transform.position, theBase.position, speed * Time.deltaTime * startspeed);
        //}
    }

    void AttackBase() {
        transform.position = Vector2.MoveTowards(transform.position, theBase.position, speed * Time.deltaTime * distanceSpeed);
    }
    void AttackPlayer() {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime * distanceSpeed);
    }
    //void Evade() {
    //    transform.position = Vector2.MoveTowards(transform.position, projectile.position, -speed * Time.deltaTime * distanceSpeed);
    //}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player.health--;
            Destroy(gameObject);
        }

        if (other.CompareTag("Projectile")) {
            player.points++;
            Destroy(other.gameObject);
            health--;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Base")) {
            player.health--;
            Destroy(gameObject);
        }
    }
}
