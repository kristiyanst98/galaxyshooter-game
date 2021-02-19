using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    private AudioSource _explosionSound;

   void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddTorque(20);
        _explosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("Explosion sound is null");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            _explosionSound.Play();
            _spawnManager.StartWave();
            Object.Destroy(other.gameObject);
            GameObject explode=Instantiate(_explosion, transform.position, Quaternion.identity);
            Object.Destroy(this.gameObject);
            Object.Destroy(explode.gameObject, 3f);
        }
    }
}
