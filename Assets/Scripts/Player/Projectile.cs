using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private EnemySpawner enemySpawner;
    private ScoreController scoreController;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        scoreController = GameObject.FindObjectOfType<ScoreController>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.CompareTag("EnemySpawn1") || collision.CompareTag("EnemySpawn2") || collision.CompareTag("EnemySpawn3"))
        {
            string enemyTag = collision.tag; // Get the tag of the enemy hit
            Destroy(collision.gameObject); // Destroy the hit enemy

            if (enemySpawner != null)
            {
                enemySpawner.ResetSpawnAvailability(enemyTag); // Tell spawner to spawn a new enemy of this tag
                scoreController.AddScore();
                GameManager.Instance.PlayKillSound();
            }
        }
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}