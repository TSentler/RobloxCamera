using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class DialogView : MonoBehaviour
    {
        public TMP_Text NameText;
        public TMP_Text MessageText;

        private void Awake()
        {
            NameText.text = "";
            MessageText.text = "";
        }

        public void SetPhrase(Phrase phrase)
        {
            NameText.text = phrase.Name;
            MessageText.text = phrase.Message;
        }
    }
}