﻿using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed;
    public Vector2 initialDirection;

    private Rigidbody2D ball;

    void Start()
    {
        ball = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
        ball.velocity = ballSpeed * initialDirection;
    }

    // upon hitting a paddle, reflect the ball. everything else is taken care (ie walls) of automatically by colliders
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            ball.velocity = ballSpeed * ComputeBounceDirection(ball.position, collision.rigidbody.position, collision.collider);
        }
        // really, really bad, but will be replaced by a proper event system for scoring during next refactoring day!
        else if (collision.gameObject.name == "LeftWall")
        {
            GameObject.Find("RightPaddle").GetComponent<AiController>().score += 1;
        }
        else if (collision.gameObject.name == "RightWall")
        {
            GameObject.Find("LeftPaddle").GetComponent<PlayerController>().score += 1;
        }
    }
    private Vector2 ComputeBounceDirection(Vector2 ballPosition, Vector2 paddlePosition, Collider2D paddleCollider)
    {
        float invertedXDirection = ballPosition.x - paddlePosition.x > 0 ? -1 : 1;
        float offsetFromPaddleCenterToBall = (ball.position.y - paddlePosition.y) / paddleCollider.bounds.size.y;
        return new Vector2(invertedXDirection, offsetFromPaddleCenterToBall).normalized;
    }
}
