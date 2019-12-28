﻿using UnityEngine;

public class AiController : MonoBehaviour
{
    public readonly float SPEED = 30;

    private Rigidbody2D ballBody;
    private Rigidbody2D paddleBody;

    void Start()
    {
        ballBody = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
        paddleBody = GameObject.FindGameObjectWithTag("Ai").GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Vector2 current = paddleBody.transform.position;
        Vector2 target = new Vector2(current.x, ballBody.transform.position.y);

        transform.position = Vector2.MoveTowards(current, target, SPEED * Time.deltaTime);
    }
}
