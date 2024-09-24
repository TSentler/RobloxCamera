using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    [RequireComponent(typeof(Collider))]
    public class UseButtonTrigger : MonoBehaviour
    {
        private UseButtonSwitcher _useButtonSwitcher;

        private void Awake()
        {
            _useButtonSwitcher = FindObjectOfType<UseButtonSwitcher>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_useButtonSwitcher != null && other.gameObject.tag == "Player")
            {
                _useButtonSwitcher.Show();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_useButtonSwitcher != null && other.gameObject.tag == "Player")
            {
                _useButtonSwitcher.Hide();
            }
        }
    }
}
