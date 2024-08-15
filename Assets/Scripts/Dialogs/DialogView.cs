using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class DialogView : MonoBehaviour
    {
        public TMP_Text NameText;
        public TMP_Text MessageText;
        public DialogButtons DialogButtons;
        public BackgroundSwitcher BackgroundSwitcher;

        private void Awake()
        {
            NameText.text = "";
            MessageText.text = "";
        }

        public void SetPhrase(Phrase phrase)
        {
            DialogButtons.InitializePhrase();
            NameText.text = phrase.Name;
            MessageText.text = phrase.Message;
            BackgroundSwitcher.Activate(phrase.BackgroundIndex);

            if (phrase is PhraseFork)
            {
                SetPhrase(phrase as PhraseFork);
            }
        }

        private void SetPhrase(PhraseFork phraseFork)
        {
            DialogButtons.InitializePhraseFork(phraseFork);
        }
    }
}