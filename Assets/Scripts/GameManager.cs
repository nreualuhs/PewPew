using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosionEffect;
    public GameObject gameOverUI;

    public int score = 0;
    public Text scoreText;
    public float respawnTime = 3.0f;
    public int lives = 3;
    public Text livesText;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            //NewGame();
             SceneManager.LoadScene(1);
        }
    }

    public void NewGame()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);

       // SetScore(0);
       // SetLives(3);
       // Respawn();
    }
/*
    public void PlayerDeath()
    {
        //explosionEffect.transform.position = player.transform.position;
        //explosionEffect.Play();

        //lives--;
        //SetLives(lives);

       // if (lives <= 0) {
       //     GameOver();
      //  } else {
      //      Invoke(nameof(Respawn), this.respawnTime);
      //  }
    }
*/
/*
    public void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
    }
    */
/*
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosionEffect.transform.position = asteroid.transform.position;
        explosionEffect.Play();

        if (asteroid.size < 0.7f) {
            SetScore(score + 100); // small asteroid
        } else if (asteroid.size < 1f) {
            SetScore(score + 50); // medium asteroid
        } else {
            SetScore(score + 25); // large asteroid
        }
    }

   */
/*
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    private void SetScore(int score)
    {
        //this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
*/
}