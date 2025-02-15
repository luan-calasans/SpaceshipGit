using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed = 5f;

    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;

    private IMovementController _movementController;
    private GunController _gunController;

    public void SetMovementController(IMovementController movementController)
    {
        _movementController = movementController;
    }

    public void SetGunController(GunController gunController)
    {
        _gunController = gunController;
    }

    public void MoveHorizontally(float x)
    {
        _movementController.MoveHorizontally(x * GetSpeed());
    }

    public void MoveVertically(float y)
    {
        _movementController.MoveVertically(y * GetSpeed());
    }

    public void ApplyFire()
    {
        // TODO: Recarregar
        _gunController.Fire();
    }

    public float GetSpeed()
    {
        // TODO: Controlar velocidade com base no estado da nave
        return speed;
    }

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
}
