using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _fallSpeed=4f;

    private Player _player;
    private Animator _anim;
    private AudioSource _explosionSound;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = 1.5f;
    private bool _shouldFire = true;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }
        _anim = GetComponent<Animator>();
        _explosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("Explosion sound is null");
        }
    }
    void Update()
    {
        CalculateMovement();
        if (Time.time > _canFire && _shouldFire)
        {
            _fireRate = Random.Range(3f, 5f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser=Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Transform[] children = enemyLaser.GetComponentsInChildren<Transform>();
            foreach(var laser in children)
            {
                laser.tag = "EnemyLaser";
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {   
            if (_player != null)
            {
               _player.Damage();
                _anim.SetTrigger("OnEnemyDeath");
                _fallSpeed = 0f;
                this.GetComponent<BoxCollider2D>().enabled = false;
                _shouldFire = false;
                _explosionSound.Play();
                Object.Destroy(this.gameObject,2.8f);
                
            }
        }
        if (other.tag == "Laser")
        {
            if (_player != null)
            {
                _player.AddScore(Random.Range(5,26));
                _anim.SetTrigger("OnEnemyDeath");
                _explosionSound.Play();
                _fallSpeed = 0f;
                GetComponent<BoxCollider2D>().enabled = false;
                _shouldFire = false;
                Object.Destroy(other.gameObject);
                Object.Destroy(this.gameObject,2.8f);
                
            }
        }
    }
   
    public void CalculateMovement()
    {
        transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float random = Random.Range(-8, 8);
            transform.position = new Vector3(random, 7, 0);
        }
    }
}
