using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private Player _player;

    void Update()
    {
        CalculateMovement();
        DestroyLaser();
    }

    void CalculateMovement()
    {
        if (this.tag=="Laser")
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }
    void DestroyLaser()
    {
        if (this.tag=="Laser")
        {
            if (transform.position.y > 8)
            {
                if (transform.parent != null)
                {
                    Object.Destroy(transform.parent.gameObject);
                }
                Object.Destroy(this.gameObject);
            }
        }
        else
        {
            if (transform.position.y < -8)
            {
                if (transform.parent != null)
                {
                    Object.Destroy(transform.parent.gameObject);
                }
                Object.Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && this.tag=="EnemyLaser")
        {
            _player = GameObject.Find("Player").GetComponent<Player>();

            if (_player == null)
            {
                Debug.Log("Player is null");
            }
            Object.Destroy(this.gameObject);
            _player.Damage();
            
        }
    }
    public void EnemyLaser()
    {
        this.tag = "EnemyLaser";
    }
}
