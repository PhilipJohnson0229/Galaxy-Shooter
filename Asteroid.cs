using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 0.5f;

    [SerializeField]
    private GameObject _explosionEffect;

    [SerializeField]
    private SpawnManager _sm;

    private void Start()
    {
        _sm = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        this.transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.tag == "Laser")
        {
            Destroy(_collision.gameObject);
            Instantiate(_explosionEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.25f);
            _sm.StartSpawning();
        }
    }
}
