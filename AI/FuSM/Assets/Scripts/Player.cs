using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public enum PlayerStates {
        evade,
        attack,
        idle,
    }
    public float speed, evadeDistance, startTimeBtwShots, timeBtwShots, shotDistance;
    public int health = 10, points = 0, maxpoints = 35, maxEvadeTime, evadeTimer, limit = 100;
    
    public Text pointsDisplay;
    public Text healthDisplay;

    public Transform enemy;
    public Transform rapidEnemy;
    public GameObject projectile;

    private Vector2 enemyPos, rapidEnemyPos;

    public PlayerStates CurrentPS;

    void Start() {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        rapidEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;        
        timeBtwShots = startTimeBtwShots;
    }

    void Update() {
        healthDisplay.text = "Health : " + health;
        pointsDisplay.text = "Points : " + points;

        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        rapidEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        
        switch (CurrentPS) {
            case PlayerStates.idle:
                if (Vector2.Distance(transform.position, enemy.position) < evadeDistance) {
                    CurrentPS = PlayerStates.evade;
                } else if (Vector2.Distance(transform.position, enemy.position) < shotDistance) {
                    CurrentPS = PlayerStates.attack;
                } else {
                    transform.position = this.transform.position;
                }
                break;
            case PlayerStates.attack:
                transform.position = Vector2.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
                if (timeBtwShots <= 0) {
                    Attack();
                } else {
                    timeBtwShots -= Time.deltaTime;
                }
                if (Vector2.Distance(transform.position, enemy.position) < evadeDistance) {
                    CurrentPS = PlayerStates.evade;
                }
                break;
            case PlayerStates.evade:
                if (Vector2.Distance(transform.position, enemy.position) < evadeDistance) {
                    transform.position = Vector2.MoveTowards(transform.position, enemy.position, -speed * Time.deltaTime);
                    if (timeBtwShots <= 0) {
                        Attack();
                    } else {
                        timeBtwShots -= Time.deltaTime;
                    }
                    evadeTimer++;
                } else {
                    CurrentPS = PlayerStates.attack;
                }
                break;
        }

        if (health <= 0 || points >= maxpoints) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Attack() {
        Instantiate(projectile, transform.position, Quaternion.identity);
        timeBtwShots = startTimeBtwShots;
    }
}
