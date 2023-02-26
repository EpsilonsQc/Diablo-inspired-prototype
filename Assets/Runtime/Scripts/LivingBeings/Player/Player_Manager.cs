using UnityEngine;
using UnityEngine.UI; // used for "Slider"
using TMPro; // used for "TextMeshProGUI"

// HOTKEYS
// H            : Receive heal [gain health]
// T            : Take Damage [lose health]
// R            : Mana regen [gain mana]
// C            : Consume mana [lose mana]
// G            : You killed a monster [gain xp]
// L            : You died against a monster [lose xp]

namespace HeroicFantasy.LivingBeings
{
    public class Player_Manager : MonoBehaviour
    {
        [Header("PLAYER STRUCT")]
        [SerializeField] private Player player; // Object of type "Player" (from struct)

        [Header("INFOS")]
        private TextMeshProUGUI playerName;
        private TextMeshProUGUI playerLevel;

        [Header("HEALTH")]
        private Slider healthBar; 
        private TextMeshProUGUI currentHealth;
        private TextMeshProUGUI maxHealth;

        [Header("MANA")]
        private Slider manaBar;
        private TextMeshProUGUI currentMana;
        private TextMeshProUGUI maxMana;

        [Header("XP")]
        private Slider xpBar;
        private TextMeshProUGUI currentEXP;
        private TextMeshProUGUI maxEXP;

        private void Awake()
        {
            // PLAYER SHEET
            playerName = GameObject.Find("Canvas (HUD)/Player (Image)/Name (Text)").GetComponent<TextMeshProUGUI>();
            playerLevel = GameObject.Find("Canvas (HUD)/Player (Image)/Level (Text)").GetComponent<TextMeshProUGUI>();

            // HEALTH
            healthBar = GameObject.Find("Canvas (HUD)/Health (Slider)").GetComponent<Slider>();
            currentHealth = GameObject.Find("Canvas (HUD)/Health (Slider)/Ressources/Current (Text)").GetComponent<TextMeshProUGUI>();
            maxHealth = GameObject.Find("Canvas (HUD)/Health (Slider)/Ressources/Max (Text)").GetComponent<TextMeshProUGUI>();

            // MANA
            manaBar = GameObject.Find("Canvas (HUD)/Mana (Slider)").GetComponent<Slider>();
            currentMana = GameObject.Find("Canvas (HUD)/Mana (Slider)/Ressources/Current (Text)").GetComponent<TextMeshProUGUI>();
            maxMana = GameObject.Find("Canvas (HUD)/Mana (Slider)/Ressources/Max (Text)").GetComponent<TextMeshProUGUI>();

            // XP
            xpBar = GameObject.Find("Canvas (HUD)/XP (Slider)").GetComponent<Slider>();
            currentEXP = GameObject.Find("Canvas (HUD)/XP (Slider)/Ressources/Current (Text)").GetComponent<TextMeshProUGUI>();
            maxEXP = GameObject.Find("Canvas (HUD)/XP (Slider)/Ressources/Max (Text)").GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            // Player CTOR : NAME, LEVEL, CURRENT HP, MAX HP, CURRENT MP, MAX MP, CURRENT XP, MAX XP, TOTAL XP
            player = new Player
            (
                playerName.text, // name
                int.Parse(playerLevel.text), // level

                (int)healthBar.value, // current HP
                (int)healthBar.maxValue, // max HP

                (int)manaBar.value, // current MP
                (int)manaBar.maxValue, // max MP

                (int)xpBar.value, // current XP
                (int)xpBar.maxValue, // max XP
                (int)xpBar.minValue // total XP
            );

            // Initialize HUD ressources text
            currentHealth.text = player.currentHP.ToString(); // current HP
            maxHealth.text = player.maxHP.ToString(); // max HP

            currentMana.text = player.currentMP.ToString(); // current MP
            maxMana.text = player.maxMP.ToString(); // max MP

            currentEXP.text = player.currentXP.ToString(); // current XP
            maxEXP.text = player.maxXP.ToString(); // max XP
        }

        private void Update()
        {
            Hotkeys(); // Manage player hotkeys in real time
        }

        #region HEALTH
        // HEALTH
        private void GainHealth(int heal)
        {
            Debug.Log("You gained " + heal + " HP.");

            player.currentHP += heal;
            healthBar.value = player.currentHP;
            currentHealth.text = player.currentHP.ToString();

            if(player.currentHP > player.maxHP) // if current HP is greater than max HP
            {
                player.currentHP = player.maxHP; // set current HP to max HP
                healthBar.value = player.currentHP; // set health bar value to max HP
                currentHealth.text = player.currentHP.ToString(); // set current HP text to max HP
            }
        }

        private void LoseHealth(int damage)
        {
            Debug.Log("You lost " + damage + " HP.");

            player.currentHP -= damage;
            healthBar.value = player.currentHP;
            currentHealth.text = player.currentHP.ToString();

            if(player.currentHP <= 0) // if HP is negative, you are dead
            {
                Debug.Log("You died.");

                player.currentHP = 1; // set HP to 1
                healthBar.value = player.currentHP; // set health bar value to 1
                currentHealth.text = player.currentHP.ToString(); // set current HP text to 1

                this.transform.position = new Vector3(960, 0, 545); // teleport player to spawn point
                this.transform.rotation = Quaternion.Euler(0, 0, 0); // reset player rotation for spawn point
            }
        }

        private void SetHealth(int health)
        {
            Debug.Log("You currently have " + health + " HP.");

            player.currentHP = health;
            healthBar.value = player.currentHP;
            currentHealth.text = player.currentHP.ToString();
        }

        private void IncreaseMaxHealth(int health)
        {
            Debug.Log("You gained +" + health + " HP.");

            player.maxHP += health;
            healthBar.maxValue = player.maxHP;
            maxHealth.text = player.maxHP.ToString();

            player.currentHP = player.maxHP;
            healthBar.value = player.maxHP;
            currentHealth.text = player.maxHP.ToString();
        }
        #endregion

        #region MANA
        // MANA
        private void GainMana(int mana)
        {
            Debug.Log("You gained " + mana + " MP.");

            player.currentMP += mana;
            manaBar.value = player.currentMP;
            currentMana.text = player.currentMP.ToString();

            if(player.currentMP > player.maxMP) // if current MP is greater than max MP
            {
                player.currentMP = player.maxMP; // set current MP to max MP
                manaBar.value = player.currentMP; // set mana bar value to max MP
                currentMana.text = player.currentMP.ToString(); // set current MP text to max MP
            }
        }

        private void LoseMana(int mana)
        {
            Debug.Log("You lost " + mana + " MP.");

            player.currentMP -= mana;
            manaBar.value = player.currentMP;
            currentMana.text = player.currentMP.ToString();

            if(player.currentMP < 0) // if MP is negative
            {
                player.currentMP = 0; // set MP to 0
                manaBar.value = player.currentMP; // set mana bar value to 0
                currentMana.text = player.currentMP.ToString(); // set current MP text to 0
            }
        }

        private void SetMana(int mana)
        {
            Debug.Log("You currently have " + mana + " MP.");

            player.currentMP = mana;
            manaBar.value = player.currentMP;
            currentMana.text = player.currentMP.ToString();
        }

        private void IncreaseMaxMana(int mana)
        {
            Debug.Log("You gained +" + mana + " MP.");

            player.maxMP += mana;
            manaBar.maxValue = player.maxMP;
            maxMana.text = player.maxMP.ToString();

            player.currentMP = player.maxMP;
            manaBar.value = player.maxMP;
            currentMana.text = player.maxMP.ToString();
        }
        #endregion

        #region XP
        // XP
        public void GainXP(int XP)
        {
            Debug.Log("You gained " + XP + " XP.");

            player.currentXP += XP;
            xpBar.value = player.currentXP;
            currentEXP.text = player.currentXP.ToString();

            player.totalXP += XP; // add XP to total XP

            if(player.currentXP > player.maxXP) // if current XP is greater than max XP
            {
                player.currentXP -= player.maxXP; // carry XP over to next level
                xpBar.value = player.currentXP; // set XP bar value to current XP
                currentEXP.text = player.currentXP.ToString(); // set current XP text to current XP

                GainLevel(); // level up
            }
        }

        private void LoseXP(int XP)
        {
            Debug.Log("You lost " + XP + " XP.");

            player.currentXP -= XP;
            xpBar.value = player.currentXP;
            currentEXP.text = player.currentXP.ToString();

            player.totalXP -= XP; // remove XP from total XP

            if(player.currentXP < 0) // if XP is negative
            {
                player.totalXP -= player.currentXP; // adjust total XP to compensate for negative XP

                player.currentXP = 0; // set XP to 0
                xpBar.value = player.currentXP; // set XP bar value to 0
                currentEXP.text = player.currentXP.ToString(); // set current XP text to 0
            }
        }

        private void SetXP(int XP)
        {
            Debug.Log("You currently have " + XP + " XP.");

            player.currentXP = XP;
            xpBar.value =  player.currentXP;
            currentEXP.text = player.currentXP.ToString();
        }

        private void IncreaseMaxXP(int XP)
        {
            Debug.Log("The next level require " + (player.maxXP + XP) + " XP.");
            Debug.Log("You currently need " + ((player.maxXP + XP) - player.currentXP) + " XP to reach the next level.");

            player.maxXP += XP;
            xpBar.maxValue = player.maxXP;
            maxEXP.text = player.maxXP.ToString();
        }
        #endregion

        #region LEVEL
        // LEVEL
        private void GainLevel()
        {
            Debug.Log("YOU HAVE GAINED A LEVEL !");
                
            IncreaseLevel(1); // increase level by 1
            IncreaseMaxHealth(10); // increase max HP by 10
            IncreaseMaxMana(5); // increase max MP by 5
            IncreaseMaxXP(500); // increase max XP by 500
        }

        private void IncreaseLevel(int level)
        {
            Debug.Log("You are now level " + (player.level + level) + " !");

            player.level += level; // set level
            playerLevel.text = player.level.ToString(); // set level text
        }
        #endregion

        #region HOTKEYS
        // HOTKEYS
        private void Hotkeys()
        {
            // HEALTH
            if(Input.GetKeyDown(KeyCode.H) && player.currentHP < player.maxHP)
            {
                GainHealth(15); // Press H to heal
            }

            if(Input.GetKeyDown(KeyCode.T) && player.currentHP > 0)
            {
                LoseHealth(15); // Press T to take damage
            }

            // MANA
            if(Input.GetKeyDown(KeyCode.R) && player.currentMP < player.maxMP)
            {
                GainMana(5); // Press R to gain mana
            }

            if(Input.GetKeyDown(KeyCode.C) && player.currentMP > 0)
            {
                LoseMana(5); // Press C to lose mana
            }

            // XP
            if(Input.GetKeyDown(KeyCode.G) && player.currentXP < player.maxXP)
            {
                GainXP(750); // Press G to gain XP
            }

            if(Input.GetKeyDown(KeyCode.L) && player.currentXP > 0)
            {
                LoseXP(750); // Press L to lose XP
            }
        }
        #endregion
    }
}