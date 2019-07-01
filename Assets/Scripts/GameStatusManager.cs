using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyGoalTarget;
    [SerializeField] private EnemyGenerator enemySpawner;
    private bool gameOver = false;

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

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(gameOver)
        {
            GUI.Box(new Rect(0, 0, 200, 50), "Game Over!");
        }
    }
}
