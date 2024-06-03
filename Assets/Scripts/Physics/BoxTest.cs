using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxTest : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _direction = Vector3.right * (Mathf.PingPong(Time.time, 3) / 1.5f - 1f);
        _rigidbody.AddForce(_direction * _speed * Time.deltaTime);
    }
}
