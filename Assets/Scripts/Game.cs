﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public enum SoundClip
    {
        Paddle,
        Block,
        GameOver
    }

    public AudioClip[] Sounds;
    public static Game Instance;
    bool actionPressed = false;
    public float direction = 0; // negative = left, positive = right
    Ball ball;
    Paddle paddle;
    int score = 0;
    int lives = 5;
    GUIStyle style;

	void Start ()
    {
        if (Instance == null) Instance = this;

        Random.seed = System.DateTime.Now.Second;
        paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.gray;
	}
	
	void Update () 
    {
        direction = Input.GetAxis("Horizontal");
	    // Read Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actionPressed = true;
        }
	}

    void LateUpdate()
    {
        if (direction != 0)
        {
            if (direction < 0)
            {
                paddle.Move(Paddle.EDirection.LEFT);
            }
            else
            {
                paddle.Move(Paddle.EDirection.RIGHT);
            }
            direction = 0;
        }

        if (actionPressed)
        {
            actionPressed = false;
            if (ball.IsResting)
                ball.Launch();
        }
    }

    public void AddScore()
    {
        ++score;
    }

    public void LoseLife()
    {
        --lives;
    }

    public void PlaySound(SoundClip sound)
    {
        audio.PlayOneShot(Sounds[(int)sound]);
    }

    void OnGUI()
    {
        GUILayout.Label(lives.ToString(), style);
        if (lives <= 0)
        {
            GUILayout.Label("YOU LOSE!!!!!!!", style);
        }
    }
}
