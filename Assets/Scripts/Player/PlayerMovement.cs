using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private float yThreshold = -10f;
    [SerializeField] private float screenBorder;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private Camera _camera;


    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;

        // Check if the player's Y position is below the threshold
        if (transform.position.y < yThreshold)
        {
            ResetPlayerPosition();
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            //PreventPlayerGoingOffScreen();
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    private void ResetPlayerPosition()
    {
        if (startingPosition != null)
        {
            // Reset the player's position to the starting position
            transform.position = startingPosition.position;
            Debug.Log("Player reset to the starting position.");
        }
        else
        {
            Debug.LogWarning("Starting position is not set.");
        }
    }
    //private void PreventPlayerGoingOffScreen()
    //{
    //    Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

    //    if ((screenPosition.x < screenBorder && body.velocity.x < 0) || 
    //        (screenPosition.x > _camera.pixelWidth - screenBorder && body.velocity.x >0))
    //    {
    //        body.velocity = new Vector2(0, body.velocity.y);
    //    }

    //    if ((screenPosition.y < screenBorder && body.velocity.y < 0) ||
    //        (screenPosition.y > _camera.pixelHeight - screenBorder && body.velocity.y > 0))
    //    {
    //        body.velocity = new Vector2(body.velocity.x, 0);
    //    }
    //}

}