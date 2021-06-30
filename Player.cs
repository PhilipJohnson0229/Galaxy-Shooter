using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //this allows us to view private variables in the inspector
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _maxHealth = 3;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private bool _isTrippleShotActive;
    [SerializeField]
    private bool _isSpeedBoosteActive;
    [SerializeField]
    private bool _isShieldActive;
    [SerializeField]
    private GameObject _shields;
    [SerializeField]
    private GameObject[] _damageFires;
    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private AudioSource _audioPlayer;

    [SerializeField]
    private AudioClip _laserShotAudio;
    

    [SerializeField]
    private int _score = 0;

    void Start()
    {
        _health = _maxHealth;
        UIManager.instance.UpdateScore(_score);
        UIManager.instance.UpdateLives(_maxHealth);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _audioPlayer = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("there is a missing spawn manager");
        }


        if (_audioPlayer == null)
        {
            Debug.LogError("there is a missing audio source on the player");
        }
        else 
        {
            _audioPlayer.clip = _laserShotAudio;
        }


        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoosteActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }
       


        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }


        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        //spawn a bullet
        

        if (_isTrippleShotActive)
        {
            Instantiate(_trippleShotPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), Quaternion.identity);
        }

        _audioPlayer.Play();
        
    }

    public void TakeDamage(int damage)
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            return;
        }
        _health = _health - damage;
        UIManager.instance.UpdateLives(_health);
        switch (_health)
        {
            case 2:
                _damageFires[0].SetActive(true);
                break;
            case 1:
                _damageFires[1].SetActive(true);
                break;
        }

        if (_health <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            UIManager.instance.ShowGameOver();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTrippleSHot()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    public void ActivateSpeedBoost()
    {
        _isSpeedBoosteActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _shields.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTrippleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBoosteActive = false;
        _speed /= _speedMultiplier;
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(7f);
        _isShieldActive = false;
        _shields.SetActive(false);
    }

    public void UpdateScore(int _scoreChange, bool increase)
    {
        if (increase == true)
        {
            _score = _score + _scoreChange;
        }
        else
        {
            _score = _score - _scoreChange;
            if (_score < 0)
            {
                _score = 0;
            }
        }

        UIManager.instance.UpdateScore(_score);
    }
}
 