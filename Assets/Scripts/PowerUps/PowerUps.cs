using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float leftLimit;
    protected abstract void PowerUpEffect(); 
    
    protected void Movement()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.left;

        if (transform.position.x <= leftLimit)
            Destroy(gameObject);
    }
}
