using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float speed = 5f;
    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        var heigth = Camera.main.orthographicSize * 2f;
        var width = heigth * Camera.main.aspect;
        var size = new Vector3(width, heigth);

        _cameraBounds = new Bounds(Vector3.zero, size);

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void LateUpdate()
    {
        var newPosition = transform.position;

        var spriteWidth = _spriteRenderer.sprite.bounds.extents.x;
        var spriteHeight = _spriteRenderer.sprite.bounds.extents.y;

        newPosition.x = Mathf.Clamp(transform.position.x, _cameraBounds.min.x + spriteWidth, _cameraBounds.max.x - spriteWidth);
        newPosition.y = Mathf.Clamp(transform.position.y, _cameraBounds.min.y + spriteHeight, _cameraBounds.max.y - spriteHeight);

        transform.position = newPosition;
    }
    void Update()
    {
        ApplyMovement();
        FireProjectile();
    }
    void FireProjectile()
    {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }

    void ApplyMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(Time.deltaTime * speed * new Vector3(horizontal, vertical));
    }
}
