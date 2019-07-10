using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    private int numberOfStuns = 0;
    
    public Transform Target
    {
        get => target;
        set => target = value;
    }
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    public int NumberOfStuns
    {
        get => numberOfStuns;
        set => numberOfStuns = value;
    }

    private Rigidbody2D enemybody;
    private Damageable damageComponenet;

    void Start()
    {
        //Store refrences for componenets
        enemybody = gameObject.GetComponent<Rigidbody2D>();
        damageComponenet = gameObject.GetComponent<Damageable>();

        Target = GameObject.FindWithTag("Target").transform;
        Vector3 dir = Target.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(NumberOfStuns == 0)
        {
            enemybody.MovePosition(enemybody.position + new Vector2(transform.right.x, transform.right.y) * Speed * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        if(Target == null)
        {
            damageComponenet.Kill();
        }
    }
}
