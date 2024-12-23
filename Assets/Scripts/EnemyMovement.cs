using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        Debug.Log("Enemy initialized with moveSpeed: " + moveSpeed);
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
        Debug.Log("Enemy flipped, new moveSpeed: " + moveSpeed);
    }

    void FlipEnemyFacing()
    {
        float currentScaleX = transform.localScale.x;
        transform.localScale = new Vector3(
            -currentScaleX, 
            transform.localScale.y, 
            transform.localScale.z
        );
    }
}
