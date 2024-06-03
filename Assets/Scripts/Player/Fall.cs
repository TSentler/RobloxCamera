using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fall : MonoBehaviour
    {
        [SerializeField] private float _rate;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(Physics.gravity * _rigidbody.mass * _rate);
        }
    }
}
