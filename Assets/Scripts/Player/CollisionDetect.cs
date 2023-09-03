using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask climbLayer;
    public LayerMask deathLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool onDeath;
    public int wallSide;

    [Space]

    [Header("Collision")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset, midOffset;
    public Color debugCollisionColor = Color.red;

    private static CollisionDetect _instance;
    public static CollisionDetect Instance { get => _instance; }

    private void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        // Ground Check
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        // Wall Check
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) ||
                 Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        // Death Check
        onDeath = Physics2D.OverlapCircle((Vector2)transform.position + midOffset, collisionRadius, deathLayer) ||
                  Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, deathLayer);

        // Right Wall Check
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);

        // Left Wall Check
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + midOffset, collisionRadius);
    }
}
