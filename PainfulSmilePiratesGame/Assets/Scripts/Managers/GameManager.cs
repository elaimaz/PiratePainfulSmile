﻿using System.Dynamic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float chaserSpawnTime = 8;
    private float lastChaserSpawned = 0;
    [SerializeField]
    private GameObject chaserShip = null;
    [SerializeField]
    private float shooterSpawnTime = 2;
    private float lastShooterSpawned = 0;
    [SerializeField]
    private GameObject shooterShip = null;
    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private LayerMask unspawnableArea = 0;

    [SerializeField]
    private int duration = 3;
    private float time;

    private UIManager uIManager;

    private int score = 0;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        time = duration * 60;
        uIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            uIManager.updateTimer(time);
        }
        else
        {
            Debug.Log("EndGame");
        }

        if (Time.time > chaserSpawnTime + lastChaserSpawned)
        {
            lastChaserSpawned = Time.time;
            if (playerPosition)
            {
                Vector3 randomPosition = new Vector3(Random.Range(playerPosition.transform.position.x - 15, playerPosition.transform.position.x + 15), Random.Range(playerPosition.transform.position.y - 10, playerPosition.transform.position.y + 10), 0);
                if (randomPosition.x > playerPosition.transform.position.x - 9f || randomPosition.x < playerPosition.transform.position.x + 9f)
                    if (randomPosition.x >= playerPosition.transform.position.x)
                        randomPosition.x += 9f;
                    else
                        randomPosition.x -= 9f;
                if (randomPosition.y > playerPosition.transform.position.y - 5.5f || randomPosition.y < playerPosition.transform.position.y + 5.5f)
                    if (randomPosition.y >= playerPosition.transform.position.y)
                        randomPosition.y += 5.5f;
                    else
                        randomPosition.y -= 5.5f;
                Instantiate(chaserShip, randomPosition, Quaternion.identity);
            }
        }

        if (Time.time > shooterSpawnTime + lastShooterSpawned)
        {
            lastShooterSpawned = Time.time;
            if (playerPosition)
            {
                Vector3 randomShooterPosition = RandomShooterPosition();
                Instantiate(shooterShip, randomShooterPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 RandomShooterPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(playerPosition.transform.position.x - 8, playerPosition.transform.position.x + 8), Random.Range(playerPosition.transform.position.y - 4, playerPosition.transform.position.y + 4), 0);
        if (randomPosition.x > playerPosition.transform.position.x - 3 || randomPosition.x < playerPosition.transform.position.x + 3)
            if (randomPosition.x >= playerPosition.transform.position.x)
                randomPosition.x += 2;
            else
                randomPosition.x -= 2;
        if (randomPosition.y > playerPosition.transform.position.y - 2 || randomPosition.y < playerPosition.transform.position.y + 2)
            if (randomPosition.y >= playerPosition.transform.position.y)
                randomPosition.y += 1;
            else
                randomPosition.y -= 1;
        if (Physics2D.OverlapCircle(randomPosition, 1, unspawnableArea) != null)
            randomPosition = RandomShooterPosition();

        return randomPosition;
    }

    public void UpdateScore()
    {
        score++;
        uIManager.UpdateScoreText(score);
    }

}
