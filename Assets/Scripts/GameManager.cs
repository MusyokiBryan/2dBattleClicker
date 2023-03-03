using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<BossData> bosses;
    public List<WeaponData> weapons;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 1f;
    public int coinsPerKill = 10;
    public Text healthText;
    public Text currentWeaponText;
    public Text coinsText;
    public Text timerText;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public float gameTimer = 60f;

    private int currentBossIndex = 0;
    private GameObject player;
    private GameObject enemy;
    private float lastSpawnTime = 0f;
    private int coins = 0;
    private float remainingTime;
    private bool gameEnded = false;

    private void Start()
    {
        remainingTime = gameTimer;
        SpawnPlayer();
        SpawnEnemy();
    }

    private void Update()
    {
        if (!gameEnded)
        {
            UpdateTimer();
            CheckGameEnd();
            CheckPlayerInput();
            SpawnNewEnemy();
        }
    }

    private void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Max(0f, Mathf.RoundToInt(remainingTime));
    }

    private void CheckGameEnd()
    {
        if (remainingTime <= 0f)
        {
            EndGame(false);
        }
        else if (bosses[currentBossIndex].currentHealth <= 0 && enemy == null)
        {
            EndGame(true);
        }
    }

    private void EndGame(bool win)
    {
        gameEnded = true;
        if (win)
        {
            winScreen.SetActive(true);
        }
        else
        {
            gameOverScreen.SetActive(true);
        }
    }

    private void CheckPlayerInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            player.GetComponent<PlayerController>().Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.GetComponent<PlayerController>().SwitchWeapon();
        }
    }

    private void SpawnNewEnemy()
    {
        if (enemy == null && lastSpawnTime + spawnDelay <= Time.time && currentBossIndex < bosses.Count)
        {
            currentBossIndex++;
            SpawnEnemy();
        }
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.bossData = bosses[currentBossIndex];
        playerController.weapons = weapons;
        playerController.currentWeaponText = currentWeaponText;
        playerController.UpdateWeaponUI();
        playerController.onDamage += OnPlayerDamaged;
    
    }

    private void SpawnEnemy()
    {
        lastSpawnTime = Time.time;
        if (currentBossIndex >= bosses.Count)
        {
            return;
        }
        enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.bossData = bosses[currentBossIndex];
        enemyController.onBossKilled += OnEnemyKilled;
    }

    private void OnPlayerDamaged()
    {
        UpdateHealthText();
        if (bosses[currentBossIndex].currentHealth <= 0)
        {
            coins += coinsPerKill;
            UpdateCoinsText();
            Destroy(enemy);
            enemy = null;
        }
        else
        {
            EndGame(false);
        }
    }

    private void OnEnemyKilled()
    {
        coins += coinsPerKill;
        UpdateCoinsText();
        Destroy(enemy);
        enemy = null;
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + bosses[currentBossIndex].currentHealth + "/" + bosses[currentBossIndex].maxHealth;
    }

    private void UpdateCoinsText()
    {
        coinsText.text = "Coins: " + coins;
    }
}

