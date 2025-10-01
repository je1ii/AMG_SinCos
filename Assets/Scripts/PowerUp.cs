using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PowerUp : MonoBehaviour
{
    public float inRange = 1f;
    public List<Sprite> sprites;

    private GameObject player;
    private Vector2 origPosition;
    private Player p;
    private PowerUpSpawner s;

    void Start()
    {
        player = GameObject.Find("Player");
        p = player.GetComponent<Player>();

        SetSprite();
    }

    void Update()
    {
        if (player != null)
        {
            origPosition = this.transform.position;
            var distance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - this.transform.position.y, 2));

            if (distance < inRange)
            {
                if (p.rocketCount == 1)
                {
                    p.ChangeRocketCount(Random.Range(2, 4));
                }
                else if (p.rocketCount < p.maxRockets)
                {
                    p.ChangeRocketCount(Random.Range(-1, 3));
                }
                else
                {
                    p.ChangeRocketCount(Random.Range(-8, -1));
                }

                if (s != null) s.PowerUpCollected();
                Destroy(this.gameObject);
            }
        }
    }

    public void SetSpawner(PowerUpSpawner spawner)
    {
        s = spawner;
    }

    public void SetSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        sr.sprite = sprites[Random.Range(0, 5)];
    }
}
