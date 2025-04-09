using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audioSrc;

    public float speed;
    private SpriteRenderer spriteRenderer;
    public Sprite[] walkSprites;
    public float frameRate = 0.1f;
    private int currentFrame;
    private float timer;
    public bool isFrozen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        
        movement = movement.normalized * (speed * Time.fixedDeltaTime);
        
        rb.MovePosition(rb.position + movement);

        if (movement.magnitude > 0.01f)
        {
            AnimateWalk();
        }
        else
        {
            ResetToIdle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Collectable"))
        {
            audioSrc.Play();
            Destroy(collidedObject.gameObject);
        }
    }
    
    void AnimateWalk()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame += 1;

            if (currentFrame >= walkSprites.Length)
            {
                currentFrame = 0;
            }
            spriteRenderer.sprite = walkSprites[currentFrame];
        }
    }

    void ResetToIdle()
    {
        currentFrame = 0;
        timer = 0f;
        if (walkSprites.Length > 0)
            spriteRenderer.sprite = walkSprites[0];
    }
}
