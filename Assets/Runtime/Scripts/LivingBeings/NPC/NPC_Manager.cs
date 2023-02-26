using UnityEngine;
using UnityEngine.UI; // used for "Slider"
using TMPro; // used for "TextMeshProGUI"

// HOTKEYS
// 1            : Take damage [lose health]
// 2            : Receive healing [gain health]

namespace HeroicFantasy.LivingBeings
{
    public class NPC_Manager : MonoBehaviour
    {
        [Header("NPC COMPONENTS")]
        [HideInInspector][SerializeField] private TextMeshProUGUI NPC_Name; // assigned manually in inspector
        [HideInInspector][SerializeField] private Slider NPC_Health; // assigned manually in inspector
        [HideInInspector][SerializeField] private int NPC_XpOnDeath; // assigned manually in inspector

        [Header("NPC STRUCT")]
        [SerializeField] private NPC NPC; // object of type "NPCs" (from struct)

        [Header("PLAYER MANAGER")]
        private Player_Manager player; // object of type "Player_Manager" (from struct)

        [Header("DISTANCE")]
        [SerializeField] private float maxDistance; // max distance to interact with an NPC
        [SerializeField] private float actualDistance; // distance between the NPC and the player
        private Transform playerTransform;
        private Vector3 playerPosition;

        private void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player_Manager>(); // get the object of type "Player_Manager"
            playerTransform = GameObject.Find("Player").transform; // get the transform of the player
        }

        private void Start()
        {
            // NPC's CTOR : NAME, CURRENT HP, MAX HP, XP GIVED
            NPC = new NPC(NPC_Name.text, (int)NPC_Health.value, (int)NPC_Health.maxValue, NPC_XpOnDeath);
            maxDistance = 150f; // max distance allowed between the NPC and the player for interactions
        }

        private void Update()
        {
            CheckDistance(); // compute the distance between the NPC and the player

            isDead(); // if NPC is dead, gain XP and reset the NPC
            LifeRegen(); // regenerate health over time
            GainHealth(); // receive healing
            TakeDamage(); // take damage (from the player)
        }

        private void CheckDistance()
        {
            playerPosition = playerTransform.position; // Get the player's position
            actualDistance = Vector3.Distance(playerPosition, transform.position); // Update the distance between the NPC and the player
        }

        // NPC is dead
        private void isDead()
        {
            if(NPC.currentHP <= 0)
            {
                Debug.Log("You killed " + NPC.name + ".");
                player.GainXP(NPC_XpOnDeath); // player gain XP

                NPC.currentHP = (int)NPC_Health.maxValue; // restore NPC current HP to the max value
                NPC_Health.value = NPC.currentHP; // restore NPC health to the max value
            }
        }

        // NPC restore health gradually
        private void LifeRegen()
        {
            if(NPC.currentHP < NPC.maxHP)
            {
                NPC_Health.value = Mathf.Lerp(NPC_Health.value, NPC_Health.maxValue, Time.deltaTime * 0.1f); // restore NPC health to the max value over time
                NPC.currentHP = (int)NPC_Health.value; // update the current HP of the NPC
            }
        }

        // NPC gain health
        private void GainHealth()
        {
            if(Input.GetKeyDown(KeyCode.Alpha2) && NPC.currentHP < NPC.maxHP)
            {
                if(actualDistance <= maxDistance) // if the player is close enough to interact with the NPC
                {
                    NPC.currentHP += 20; // NPC gain health
                    NPC_Health.value = NPC.currentHP; // update the current HP of the NPC

                    if(NPC.currentHP > NPC.maxHP) // if the NPC's current HP is greater than the max HP, set it to the max HP
                    {
                        NPC.currentHP = NPC.maxHP; // set the NPC's current HP to the max HP
                        NPC_Health.value = NPC.currentHP; // set the NPC's health to the max HP
                    }
                }
            }
        }

        // NPC lose health
        private void TakeDamage()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && NPC.currentHP > 0)
            {
                if(actualDistance <= maxDistance) // if the player is close enough to the NPC
                {
                    NPC.currentHP -= 20; // NPC lose health
                    NPC_Health.value = NPC.currentHP; // update the current HP of the NPC

                    if(NPC.currentHP < 0) // if NPC current HP is negative
                    {
                        NPC.currentHP = 0; // set NPC current HP to 0
                        NPC_Health.value = 0; // set NPC health to 0
                    }
                }
            }
        }
    }
}