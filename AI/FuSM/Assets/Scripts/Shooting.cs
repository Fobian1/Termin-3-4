using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject shot;
    private Transform playerPos;
    private float shotTimer;

    void Start() {
        playerPos = GetComponent<Transform>();   
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shotTimer <= 0) {
            Instantiate(shot, playerPos.position, Quaternion.identity);
        } else {
            shotTimer -= Time.deltaTime;
        }
    }
}
