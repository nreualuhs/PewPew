using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    private bool thrusting;
    private float turnDirection;
    private Rigidbody2D rb;
    public float speed = 1.0f;
    public float turnSpeed = 0.1f;
    public ParticleSystem explosion;

    public GameObject gameOverUI;
    public AudioClip shootSound;
    public AudioSource audioSource;
    [SerializeField] private AudioSource yesSource;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameOverUI.SetActive(false);
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        thrusting = Input.GetKey(KeyCode.W);
        if(Input.GetKey(KeyCode.A))
        {
            turnDirection = 1.0f;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            turnDirection = -0.1f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(shootSound, 1);
            Shoot();
        }

     if (transform.position.x > (9f + (GetComponent<SpriteRenderer>().bounds.size.x / 2)))
        {
            transform.position = new Vector3(-(9f + (GetComponent<SpriteRenderer>().bounds.size.x / 2)), transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -(9f + (GetComponent<SpriteRenderer>().bounds.size.x / 2)))
        {
            transform.position = new Vector3((9f + (GetComponent<SpriteRenderer>().bounds.size.x / 2)), transform.position.y, transform.position.z);
        }
        else if (transform.position.y > (5f + (GetComponent<SpriteRenderer>().bounds.size.y / 2)))
        {
            transform.position = new Vector3(transform.position.x, -(5f + (GetComponent<SpriteRenderer>().bounds.size.y / 2)), transform.position.z);
        }
        else if (transform.position.y < -(5f + (GetComponent<SpriteRenderer>().bounds.size.y / 2)))
        {
            transform.position = new Vector3(transform.position.x, (5f + (GetComponent<SpriteRenderer>().bounds.size.y / 2)), transform.position.z);
        }

         if (Input.GetKey(KeyCode.R)) {
            Time.timeScale = 1f;
            transform.position = new Vector3(0f, 0f, 0f);
            gameOverUI.SetActive(false);
        }
    }

    private void FixedUpdate() {
        if(thrusting)
        {
            rb.AddForce(this.transform.up * speed);
        }
        if(turnDirection != 0.0f)
        {
            rb.AddTorque(turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Asteroid"))
        {
            this.yesSource.Play();
            explosion.transform.position = transform.position;
            this.explosion.Play();
            //this.gameObject.SetActive(false);
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            //FindObjectOfType<GameManager>().PlayerDeath();
        }
    }
}
