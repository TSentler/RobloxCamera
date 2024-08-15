using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogs
{
    public class BackgroundSwitcher : MonoBehaviour
    {
        public List<GameObject> Backgrounds = new List<GameObject>();

        private void Awake()
        {
            DeactivateAll();
        }

        public void Activate(int i)
        {
            if (i == -1)
                return;

            if (i == -2)
            {
                DeactivateAll();
                return;
            }

            DeactivateAll();
            Backgrounds[i].SetActive(true);
        }

        private void DeactivateAll()
        {
            foreach (GameObject back in Backgrounds)
            {
                back.SetActive(false);
            }
        }
    }
}