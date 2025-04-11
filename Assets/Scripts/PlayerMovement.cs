using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audioSrc;
    public Animator animator;
    public float speed;
    public bool isFrozen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            animator.SetBool("frozen", isFrozen);
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = movement.normalized * (speed * Time.fixedDeltaTime);

        rb.MovePosition(rb.position + movement);

        animator.SetFloat("speed", Mathf.Abs(movement.magnitude));
    }

    private void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Collectable"))
        {
            audioSrc.Play();
            Destroy(collidedObject.gameObject);
        }
    }
}