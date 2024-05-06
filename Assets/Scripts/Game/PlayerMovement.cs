﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float x_Movement;
    public float y_Movement;
    [SerializeField] private AudioSource eat;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource hurt;
    [SerializeField] private AudioSource cofee;
    private Rigidbody2D myRB;
    [SerializeField]
    private float speed;
    private float limitSuperior;
    private float limitInferior;
    public int player_lives = 4;
    public int point = 0;
    public PointsScriptable pointsscriptable;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }
    void FixedUpdate()
    {
        float newYPosition = Mathf.Clamp(myRB.position.y + y_Movement * speed * Time.fixedDeltaTime, limitInferior, limitSuperior);
        myRB.MovePosition(new Vector2(myRB.position.x + x_Movement * speed * Time.fixedDeltaTime, newYPosition));

    }
    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -bounds.y;
        limitSuperior = bounds.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Candy")
        {
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this);
            eat.Play();
            point = point + 1;
            pointsscriptable.points = point;
        }
        if (other.tag == "Enemy")
        {
            hit.Play();
            hurt.Play();
            player_lives = player_lives - 1;
            Destroy(other.gameObject);
            transform.position = new Vector2(transform.position.x, 0);
            if (player_lives <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        if (other.tag == "Candy+")
        {
            eat.Play();
            point = point + 2;
            pointsscriptable.points = point;
            Destroy(other.gameObject);
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        x_Movement = context.ReadValue<Vector2>().x;
        y_Movement = context.ReadValue<Vector2>().y;
    }
}
