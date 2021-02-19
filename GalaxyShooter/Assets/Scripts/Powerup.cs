using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerupID; // 0 - triple shot, 1 - speed, 2 - shield
    private AudioSource _powerupSound;


    private void Start()
    {
        _powerupSound = GameObject.Find("PowerupSound").GetComponent<AudioSource>();
        if(_powerupSound == null)
        {
            Debug.LogError("PowerupSound is null");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _powerupSound.Play();
            Object.Destroy(this.gameObject);
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActivation();
                        break;
                    case 1:
                        player.SpeedUp();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                }
            }
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
