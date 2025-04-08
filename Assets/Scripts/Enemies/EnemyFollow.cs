using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
   [SerializeField] private float speed;
    private Transform target;

    private Animation animationGolbin;
    private Rigidbody2D rb;

    private bool facingLeft = false;
    private bool isDashing = false;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animationGolbin = GetComponent<Animation>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
