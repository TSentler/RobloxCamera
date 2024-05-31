using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePause
{
    public class PauseHandler : MonoBehaviour
    {
        private List<IPausable> _pausables = new ();

        public bool IsPause { get; private set; }

        public void Subscribe(IPausable subscriber)
        {
            if (IsPause)
                subscriber.Pause();
            
            _pausables.Add(subscriber);
        }

        public void UnSubscribe(IPausable subscriber)
        {
            _pausables.Remove(subscriber);
        }
        
        public void Pause()
        {
            IsPause = true;
            
            foreach (IPausable pausable in _pausables)
            {
                pausable.Pause();
            }
        }

        public void Unpause()
        {
            IsPause = false;
            
            foreach (IPausable pausable in _pausables)
            {
                pausable.Unpause();
            }
        }
    }
}