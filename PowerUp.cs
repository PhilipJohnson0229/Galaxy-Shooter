using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private int _powerUpIdentifier;

    [SerializeField]
    private AudioClip _collectionAudio;

   
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -4.5f) { }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_collectionAudio, transform.position);

            if (player != null)
            {
                switch (_powerUpIdentifier)
                {
                    case 0:
                        player.ActivateTrippleSHot();
                            break;
                    case 1:
                        player.ActivateSpeedBoost();
                            break;
                    case 2:
                        player.ActivateShield();
                            break;
                }
               
            }
            
            Destroy(this.gameObject);
            
        }
    }
}
