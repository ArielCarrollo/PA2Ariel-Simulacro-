using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float x_Movement;
    public float y_Movement;
    private Vector2 keyboardInput;
    private Vector2 mouseInput;
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
    public float Distance = 0;
    public PointsScriptable pointsscriptable;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        point = 0;
        Distance = 0;
        SetMinMax();
    }
    void Update()
    {
        Distance = Distance + Time.deltaTime;
    }
    void FixedUpdate()
    {
        Vector2 totalInput = keyboardInput + mouseInput;
        Vector2 movement = totalInput.normalized * speed * Time.fixedDeltaTime;
        myRB.MovePosition(myRB.position + movement);
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
            AddPoints(10);
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
                point = point + (int)Distance;
                SaveScore();
                SceneManager.LoadScene("GameOver");
            }
        }
        if (other.tag == "Candy+")
        {
            eat.Play();
            point = point + 2;
            AddPoints(20);
            Destroy(other.gameObject);
        }
    }
    public void OnKeyboardMovement(InputAction.CallbackContext context)
    {
        keyboardInput = context.ReadValue<Vector2>();
    }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
    void AddPoints(int pointsToAdd)
    {
        point += pointsToAdd; 
    }

    public void SaveScore()
    {
        pointsscriptable.AddHighScore(point);
    }
}
