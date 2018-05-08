using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode
{
    public class WeaponButton : MonoBehaviour
    {
        private WeaponListData weapon;

        public WeaponListData Weapon
        {
            get { return weapon; }
            set
            {
                weapon = value;
                text.text = value.displayName;
            }
        }

        [SerializeField] private Text text;
        [SerializeField] private Image image;
    }
}

