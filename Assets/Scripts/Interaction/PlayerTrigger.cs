using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class PlayerTrigger : MonoBehaviour
    {
        public UnityEvent OnEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                OnEnter.Invoke();// � ������� ����� ������ �� ���������� ����� ������ ������� 
            }
        }

    }
}