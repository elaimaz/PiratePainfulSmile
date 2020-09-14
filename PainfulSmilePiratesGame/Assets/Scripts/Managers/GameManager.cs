using System.Dynamic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Chaser Enemy")]
    [SerializeField]
    private float chaserSpawnTime = 8;
    private float lastChaserSpawned = 0;
    [SerializeField]
    private GameObject chaserShip = null;
    [SerializeField]
    private float maxChaserSpawnPos = 15;
    [SerializeField]
    private float minChaserSpawnPos = 9;
    [Header("Shooter Enemy")]
    [SerializeField]
    private float shooterSpawnTime = 2;
    private float lastShooterSpawned = 0;
    [SerializeField]
    private GameObject shooterShip = null;
    [SerializeField]
    private float maxShooterSpawnPos = 8;
    [SerializeField]
    private float minShooterSpawnPos = 4;
    private float sessionTime;
    [Header("Player Reference")]
    private Transform playerPosition;
    [Header("Unspanawble Area")]
    [SerializeField]
    private LayerMask unspawnableArea = 0;
    [Header("Session Duration")]
    [SerializeField]
    private int duration = 3;
    private float time;

    private UIManager uIManager;

    private int score = 0;
    private bool runGame = true;
    [HideInInspector]
    public bool playerAlive = true;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        duration = Data.Duration;
        chaserSpawnTime = Data.ChaserRate;
        shooterSpawnTime = Data.ShooterRate;
        time = duration * 60;
        uIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        sessionTime = 0;
    }

    private void Update()
    {
        sessionTime += Time.deltaTime;

        if (time > 0 && playerAlive == true)
        {
            time -= Time.deltaTime;
            uIManager.updateTimer(time);
        }
        else
        {
            if (runGame == true || playerAlive == false)
            {
                runGame = false;
                uIManager.EndScreen();
                uIManager.ShowFinalScore(score);
            }
        }

        if (sessionTime > chaserSpawnTime + lastChaserSpawned)
        {
            lastChaserSpawned = sessionTime;
            if (playerPosition)
            {
                Vector3 randomPosition = RandomChaserPosition();
                Instantiate(chaserShip, randomPosition, Quaternion.identity);
            }
        }

        if (sessionTime > shooterSpawnTime + lastShooterSpawned)
        {
            lastShooterSpawned = sessionTime;
            if (playerPosition)
            {
                Vector3 randomShooterPosition = RandomShooterPosition();
                Instantiate(shooterShip, randomShooterPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 RandomChaserPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(playerPosition.transform.position.x - maxChaserSpawnPos, playerPosition.transform.position.x + maxChaserSpawnPos), Random.Range(playerPosition.transform.position.y - maxChaserSpawnPos, playerPosition.transform.position.y + maxChaserSpawnPos), 0);
        if (randomPosition.x > playerPosition.transform.position.x - minChaserSpawnPos || randomPosition.x < playerPosition.transform.position.x + minChaserSpawnPos)
            if (randomPosition.x >= playerPosition.transform.position.x)
                randomPosition.x += minChaserSpawnPos;
            else
                randomPosition.x -= minChaserSpawnPos;
        if (randomPosition.y > playerPosition.transform.position.y - minChaserSpawnPos || randomPosition.y < playerPosition.transform.position.y + minChaserSpawnPos)
            if (randomPosition.y >= playerPosition.transform.position.y)
                randomPosition.y += minChaserSpawnPos;
            else
                randomPosition.y -= minChaserSpawnPos;
        if (Physics2D.OverlapCircle(randomPosition, 1, unspawnableArea) != null)
            randomPosition = RandomShooterPosition();

        return randomPosition;
    }

    private Vector3 RandomShooterPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(playerPosition.transform.position.x - maxShooterSpawnPos, playerPosition.transform.position.x + maxShooterSpawnPos), Random.Range(playerPosition.transform.position.y - minShooterSpawnPos, playerPosition.transform.position.y + minShooterSpawnPos), 0);
        if (randomPosition.x > playerPosition.transform.position.x - minShooterSpawnPos || randomPosition.x < playerPosition.transform.position.x + minShooterSpawnPos)
            if (randomPosition.x >= playerPosition.transform.position.x)
                randomPosition.x += 2;
            else
                randomPosition.x -= 2;
        if (randomPosition.y > playerPosition.transform.position.y - minShooterSpawnPos || randomPosition.y < playerPosition.transform.position.y + minShooterSpawnPos)
            if (randomPosition.y >= playerPosition.transform.position.y)
                randomPosition.y += 2;
            else
                randomPosition.y -= 2;
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
