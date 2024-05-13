using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float zLimit;
    public float bulletDamage;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.position.x >= rightLimit || rb.position.x <= leftLimit || Mathf.Abs(rb.position.z) >= zLimit)
            Destroy(gameObject);
    }
}
