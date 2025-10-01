using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 6f;
    private Vector2 direction;

    void Update()
    {
        this.transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // auto-destroy when off screen
    }
}
