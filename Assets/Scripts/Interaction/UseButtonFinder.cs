using UnityEngine;

namespace Interactions
{
    public class UseButtonFinder : MonoBehaviour
    {
        [SerializeField] private UseButton _useButton;

        public UseButton UseButton => _useButton;
    }
}