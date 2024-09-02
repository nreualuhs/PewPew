using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float size = 1.0f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 15.0f;
    public float maxLifetime = 15.0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Assign random properties to make each asteroid feel unique
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // Set the scale and mass of the asteroid based on the assigned size so
        // the physics is more realistic
        transform.localScale = Vector3.one * size;
        rigidbody.mass = this.size;

        // Destroy the asteroid after it reaches its max lifetime
        //Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        // The asteroid only needs a force to be added once since they have no
        // drag to make them stop moving
        rigidbody.AddForce(direction * movementSpeed);
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Check if the asteroid is large enough to split in half
            // (both parts must be greater than the minimum size)
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            Destroy(this.gameObject);
            
        }
    }

    private void CreateSplit()
    {
        // Set the new asteroid poistion to be the same as the current asteroid
        // but with a slight offset so they do not spawn inside each other
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Create the new asteroid at half the size of the current
        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;

        // Set a random trajectory
        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }

    public void Update()
    {
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

    }
}