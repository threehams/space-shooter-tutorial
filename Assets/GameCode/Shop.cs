using UnityEngine;
using UnityEngine.UI;

namespace GameCode
{
    public class Shop : MonoBehaviour
    {
        public GameObject playerPoint;

        [SerializeField] private WeaponDatabase weaponDatabase;
        [SerializeField] private GameObject buttonPanel;
        [SerializeField] private GameObject weaponButton;

        private Player player;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            player = FindObjectOfType<Player>();
            foreach (var weapon in weaponDatabase)
            {
                var button = Instantiate(weaponButton, buttonPanel.transform);
                button.GetComponent<WeaponButton>().Weapon = weapon;
                var localWeapon = weapon;
                button.GetComponent<Button>().onClick.AddListener(delegate { onSelectWeapon(localWeapon); });
            }
        }

        public void onDowngrade()
        {
        }

        public void onUpgrade()
        {
        }

        public void onSelectWeapon(WeaponListData weapon)
        {
            
        }
    }
}
