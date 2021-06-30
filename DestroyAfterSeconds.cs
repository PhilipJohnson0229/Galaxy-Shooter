using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float _seconds = 2f;
    

    void Start()
    {
        Destroy(this.gameObject, _seconds);
    }


}
