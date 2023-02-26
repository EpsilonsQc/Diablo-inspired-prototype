using UnityEngine;

namespace HeroicFantasy.LivingBeings
{
    [System.Serializable]
    public struct NPC
    {
        [Header("INFOS")]
        public string name;

        [Header("HEALTH")]
        public int currentHP;
        public int maxHP;

        [Header("XP")]
        public int xpOnDeath;

        // Player CTOR
        public NPC
        (
            // PARAMETERS

            // Infos
            string pName,

            // Health
            int pCurrentHP,
            int pMaxHP,

            // XP
            int pXP
        )
        {
            // INITIALIZATION

            // Infos
            name = pName;

            // Health
            currentHP = pCurrentHP;
            maxHP = pMaxHP;

            // XP
            xpOnDeath = pXP;
        }
    }
}