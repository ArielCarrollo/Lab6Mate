using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Planet;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnYPosition = 10f;
    [SerializeField] private float spawnZPosition = 50f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private Transform playerTransform;
    public SO scoreData;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 5f);
        Vector3 directionToPlayer = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        enemyPrefab.GetComponent<Rigidbody>().velocity = directionToPlayer * speed;
        scoreData.ResetScore();
        InvokeRepeating("IncrementScoreOverTime", 1f, 1f);
    }
    void Update()
    {
        Planet.transform.Rotate(rotationSpeed * Time.deltaTime,0,0);
        scoreText.text = "Score: " + scoreData.score.ToString();
    }
    void SpawnEnemy()
    {
        float randomXPosition = Random.Range(-10f, 10f);
        Vector3 spawnPosition = new Vector3(randomXPosition, spawnYPosition, spawnZPosition);
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Vector3 directionToPlayer = (playerTransform.position - enemyInstance.transform.position).normalized;


        Rigidbody enemyRigidbody = enemyInstance.GetComponent<Rigidbody>();
        enemyRigidbody.velocity = directionToPlayer * rotationSpeed;
    }
    void IncrementScoreOverTime()
    {
        scoreData.IncrementScore(1);
    }
    public void PlayerDied()
    {
        scoreData.ResetScore();
    }
}
