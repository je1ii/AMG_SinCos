using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public GameObject rocketPrefab;
    public int rocketCount = 4;
    public int maxRockets = 8;
    public float fireInterval = 3f;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        if (this.transform != null) Move();
    }

    public void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(hInput, vInput, 0).normalized;
        this.transform.position += moveDir * moveSpeed * Time.deltaTime;

        DetectMovement(moveDir);

        if (hInput > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (hInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void DetectMovement(Vector3 moveDir)
    {
        if (moveDir != null)
        {
            animator.SetFloat("Speed", moveDir.magnitude);
        }
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            FireRockets();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    public void FireRockets()
    {
        animator.SetTrigger("Fire");
        float angleStep = 360f / rocketCount;

        for (int i = 0; i < rocketCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            GameObject rocket = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
            rocket.GetComponent<Rocket>().SetDirection(dir);
        }
    }

    public void ChangeRocketCount(int amount)
    {
        rocketCount = Mathf.Clamp(rocketCount + amount, 1, maxRockets);
    }
}
