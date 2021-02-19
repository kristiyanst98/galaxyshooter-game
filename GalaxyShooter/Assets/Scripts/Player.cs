using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7f;
    private float _speedMultiplier = 1.7f;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActivated = false;
    [SerializeField]
    private GameObject _shieldPrefab;
    private bool _hasShield = false;
    [SerializeField]
    private int _score=0;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject[] _hurtEngines;
    private AudioSource _laserShot;
    private AudioSource _explosionSound;
    [SerializeField]
    private GameObject _onDeathExposion;


    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UIManagerCanvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is null");
        }
        _laserShot = GameObject.Find("LaserShot").GetComponent<AudioSource>();

        if(_laserShot == null)
        {
            Debug.LogError("Laser Shot Sound is null");
        }
        _explosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        if(_explosionSound == null)
        {
            Debug.LogError("Explosion sound is null");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Fire();
        }
    }

    void CalculateMovement()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
       
            transform.Translate(_horizontalInput * _speed * Time.deltaTime, _verticalInput * _speed * Time.deltaTime, 0);
      
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, transform.position.z);
        }
        if (transform.position.x >= 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }

    }

    void Fire()
    {
            _canFire = Time.time + _fireRate;
        if (_isTripleShotActivated)
        {
            Instantiate(_tripleShotPrefab,transform.position,Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z), Quaternion.identity);

        }
        _laserShot.Play();
    }

    public void Damage()
    {
        if (_hasShield)
        {
            _shieldPrefab.SetActive(false);
            _hasShield = false;
            return;
        }
        else
        {
            _lives -= 1;

            if(_lives == 2)
            {
                _hurtEngines[Random.Range(0, 2)].SetActive(true);
            }
            if (_lives == 1)
            {
                foreach(var engine in _hurtEngines)
                {
                    engine.SetActive(true);
                }
            }
           

            if (_lives < 1)
            {
                _lives = 0;
                _uiManager.UpdateLives(_lives);
                _uiManager.GameOverSequence();
                _spawnManager.OnPlayerDeath();
                _explosionSound.Play();
                GameObject explosion = Instantiate(_onDeathExposion, transform.position, Quaternion.identity);
                Object.Destroy(explosion, 3f);
                Object.Destroy(this.gameObject);
            }
            _uiManager.UpdateLives(_lives);
        }


    }
    public void TripleShotActivation()
    {
        _isTripleShotActivated = true;
        StartCoroutine(StopTripleShot());
    }

    IEnumerator StopTripleShot()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActivated = false;
    }
    public void SpeedUp()
    {
       
        _speed *= _speedMultiplier;
        StartCoroutine(StopSpeedUp());
    }
    IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(5f);
        _speed = 5f;
    }
    public void ActivateShield()
    {
        _hasShield = true;
        _shieldPrefab.SetActive(true);
    }
    public void AddScore(int points)
    {
        
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
