using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int speed;
    public Transform _position;

    // Update is called once per frame
    void Update()
    {
        _position = this.transform;
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (_position.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
           
            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {

            Destroy(this.gameObject);
        }
    }
}
