using UnityEngine;
using UnityEngine.UI;

namespace GameCode
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Image image;

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                text.text = value;
            }
        }
        private Sprite icon;
        public Sprite Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                image.sprite = value;
            }
        }

    }
}

