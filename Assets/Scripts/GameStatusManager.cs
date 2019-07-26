using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyGoalTarget;
    [SerializeField] private EnemyGenerator enemySpawner;
    private bool gameOver = false;
    private int parts = 0;
    private Damageable playerHealth;

    public static GameStatusManager Instance;

    public GameObject EnemyGoalTarget
    {
        get => enemyGoalTarget;
        set => enemyGoalTarget = value;
    }
    public EnemyGenerator EnemySpawner
    {
        get => enemySpawner;
        set => enemySpawner = value;
    }
    public int Parts
    {
        get => parts;
        set => parts = value;
    }


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerHealth = enemyGoalTarget.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyGoalTarget == null)
        {
            gameOver = true;
        }
        if(gameOver)
        {
            enemySpawner.ContinueSpawning = false;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), "Parts: " + Parts, "Box");
        GUI.Label(new Rect(0, 31, 100, 30), "Health: " + playerHealth.CurrentHealth, "Box");
        if(gameOver)
        {
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 200, 50), "Game Over!");
        }
    }
}
