using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private float _randX;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private int _damagePower = 1;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private AudioSource _audioPlayer;
    [SerializeField]
    private AudioClip _clipToPlay;

    void Start()
    {
        _randX = Random.Range(-8f, 8f);
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioPlayer = GetComponent<AudioSource>();
        _audioPlayer.clip = _clipToPlay;
    }

   
    void Update()
    {
        //travel downards at 4 meters a second
        transform.Translate(-Vector3.up * _speed * Time.deltaTime);

        //respawn at random x position
        if (transform.position.y <= -6)
        {
            _randX = Random.Range(-8f, 8f);

            transform.position = new Vector3(_randX, 6f, 0);
        }
        //if the enemy reaches the bottom then respawn at the top
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            _player.TakeDamage(_damagePower);
            _player.UpdateScore(10, false);
            _anim.SetBool("isDead", true);
            Collider2D _collider = GetComponent<Collider2D>();
            _collider.enabled = false;
            _audioPlayer.Play();
        }

        if (other.tag == "Laser")
        {
            _player.UpdateScore(10, true);
            _anim.SetBool("isDead", true);
            Collider2D _collider = GetComponent<Collider2D>();
            _collider.enabled = false;
            _audioPlayer.Play();
        }
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
