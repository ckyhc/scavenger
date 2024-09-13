using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    private Bounds playerBounds;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector3 movement; // Stores the direction of player movement
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        var minX = Globals.WorldBounds.min.x;
        var maxX = Globals.WorldBounds.max.x;

        var minY = Globals.WorldBounds.min.y;
        var maxY = Globals.WorldBounds.max.y;

        playerBounds = new Bounds();
        playerBounds.SetMinMax(
            new Vector3(minX, minY, this.transform.position.z),
            new Vector3(maxX, maxY, this.transform.position.z)
            );
    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Speed", horizontalInput);

        if (horizontalInput < 0)
        {
            this.spriteRenderer.flipX = true;
        }
        if (horizontalInput > 0)
        {
            this.spriteRenderer.flipX = false;
        }

        Debug.Log("!!!" + horizontalInput);

        if (horizontalInput > playerBounds.max.x || horizontalInput < playerBounds.min.x){
          horizontalInput = 0;
        }
        if(verticalInput > playerBounds.max.y || verticalInput < playerBounds.min.y){
          verticalInput = 0;
        }

        movement = new Vector3(horizontalInput, verticalInput, transform.position.z);
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }
}
