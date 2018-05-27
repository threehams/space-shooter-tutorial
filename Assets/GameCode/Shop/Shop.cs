using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode
{
    public class Shop : MonoBehaviour
    {
        public GameObject playerPoint;

        [SerializeField] private WeaponDatabase weaponDatabase;
        [SerializeField] private GameObject weaponPanel;
        [SerializeField] private GameObject weaponActionsPanel;
        [SerializeField] private GameObject shopButtonPrefab;
        [SerializeField] private GameObject hardpointPanel;
        [SerializeField] private GameObject fireButton;
        [SerializeField] private GameObject upgradeButton;
        [SerializeField] private GameObject downgradeButton;
        [SerializeField] private GameObject backButton;
        [SerializeField] private GameObject nextLevelButton;

        private Hardpoint selectedHardpoint;
        private Player player;
        private ShopPlayerAi playerAi;
        private Game game;

        private void Awake()
        {
            gameObject.SetActive(false);
            hardpointPanel.SetActive(true);
            weaponPanel.SetActive(false);
            weaponActionsPanel.SetActive(false);
            upgradeButton.GetComponent<Button>().onClick.AddListener(OnUpgrade);
            downgradeButton.GetComponent<Button>().onClick.AddListener(OnDowngrade);
            backButton.GetComponent<Button>().onClick.AddListener(OnBack);
            nextLevelButton.GetComponent<Button>().onClick.AddListener(OnNextLevel);
        }

        private void OnEnable()
        {
            player = FindObjectOfType<Player>();
            game = FindObjectOfType<Game>();
            playerAi = FindObjectOfType<ShopPlayerAi>();
            fireButton.GetComponent<Button>().onClick.AddListener(() => playerAi.fire = !playerAi.fire);
            hardpointPanel.SetActive(true);
            weaponPanel.SetActive(false);
            weaponActionsPanel.SetActive(false);
            PopulateHardpoints(player.hardpoints);
        }

        private void PopulateWeapons(IEnumerable<WeaponListData> weapons)
        {
            foreach (Transform child in weaponPanel.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var weapon in weapons)
            {
                var button = Instantiate(shopButtonPrefab, weaponPanel.transform);
                var shopButton = button.GetComponent<ShopButton>();
                shopButton.DisplayName = weapon.displayName;
                button.GetComponent<Button>().onClick.AddListener(() => OnSelectWeapon(weapon));
            }
        }

        private void PopulateHardpoints(IEnumerable<Hardpoint> hardpoints)
        {
            foreach (Transform child in hardpointPanel.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var hardpoint in hardpoints)
            {
                var button = Instantiate(shopButtonPrefab, hardpointPanel.transform);
                button.GetComponent<ShopButton>().DisplayName = hardpoint.displayName;
                button.GetComponent<Button>().onClick.AddListener(() => OnSelectHardpoint(hardpoint));
            }
        }

        private void OnDowngrade()
        {
            var current = selectedHardpoint.weapon;
            if (current.level == 0)
            {
                return;
            }

            selectedHardpoint.ReplaceWeapon(current.weaponListData.weaponLevels[current.level - 1]);
            game.AddCash(current.cost);
        }

        private void OnUpgrade()
        {
            var current = selectedHardpoint.weapon;
            if (current.level == current.weaponListData.weaponLevels.Length - 1)
            {
                return;
            }

            var next = current.weaponListData.weaponLevels[current.level + 1];
            if (next == null || next.cost > game.cash.Value)
            {
                return;
            }

            selectedHardpoint.ReplaceWeapon(next);
            game.RemoveCash(next.cost);
        }

        private void OnSelectWeapon(WeaponListData selectedWeapon)
        {
            // not allowing preview mode - you click it, you buy it
            var current = selectedHardpoint.weapon;
            var currentValue = 0;
            if (current != null)
            {
                currentValue = weaponDatabase.TotalCost(current.weaponListData, current.level);
            }

            var newWeapon = selectedWeapon.weaponLevels[0];

            // can't afford it even after selling your current weapon
            if (currentValue + game.cash.Value < newWeapon.cost)
            {
                return;
            }

            selectedHardpoint.ReplaceWeapon(newWeapon);
            game.RemoveCash(newWeapon.cost - currentValue);
        }

        private void OnBack()
        {
            hardpointPanel.SetActive(true);
            weaponPanel.SetActive(false);
            weaponActionsPanel.SetActive(false);
            selectedHardpoint = null;
            PopulateHardpoints(player.hardpoints);
        }

        private void OnSelectHardpoint(Hardpoint hardpoint)
        {
            hardpointPanel.SetActive(false);
            weaponPanel.SetActive(true);
            weaponActionsPanel.SetActive(true);
            selectedHardpoint = hardpoint;
            PopulateWeapons(weaponDatabase.weapons.Where(weapon => weapon.hardpoint == hardpoint.type));
        }

        private void OnNextLevel()
        {
            game.StartNextLevel();
        }
    }
}
