using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    [SerializeField] private string bulletSpawnPoolName;
    [SerializeField] private float shotInterval;
    
    public string BulletSpawnPoolName
    {
        get => bulletSpawnPoolName;
        set => bulletSpawnPoolName = value;
    }
    public float ShotInterval
    {
        get => shotInterval;
        set => shotInterval = value;
    }

    private IEnumerator shootingRoutine = null;
    private List<Damageable> enemyTargets = new List<Damageable>();

    void Start()
    {
        shootingRoutine = StartShootRoutine();
        StartCoroutine(shootingRoutine);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            enemyTargets.Add(col.gameObject.GetComponent<Damageable>());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            enemyTargets.Remove(col.gameObject.GetComponent<Damageable>());
        }
    }

    public IEnumerator StartShootRoutine()
    {
        while(true){
            Damageable shootTarget = null;
            float targetDistanceSquare = float.PositiveInfinity;
            for(int i = 0; i < enemyTargets.Count; i++)
            {
                if(enemyTargets[i] == null)
                {
                    enemyTargets.Remove(enemyTargets[i]);
                }
                if((enemyTargets[i].transform.position - transform.position).sqrMagnitude < targetDistanceSquare)
                {
                    targetDistanceSquare = (enemyTargets[i].transform.position - transform.position).sqrMagnitude;
                    shootTarget = enemyTargets[i];
                }
            }
            if(shootTarget != null)
            {
                GameObject nextBullet = ObjectPool.Instance.SpawnObject(BulletSpawnPoolName, transform.position, Quaternion.identity);
                nextBullet.GetComponent<EnemyMovement>().Target = shootTarget.transform;
            }
            yield return new WaitForSeconds(ShotInterval);
        }
    }
    
}
