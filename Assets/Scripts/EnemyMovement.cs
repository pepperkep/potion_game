using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float speed = 5f;
    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    private Rigidbody2D enemybody;

    void Start()
    {
        enemybody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemybody.MovePosition(enemybody.position + Direction * Speed * Time.fixedDeltaTime);
    }
}
