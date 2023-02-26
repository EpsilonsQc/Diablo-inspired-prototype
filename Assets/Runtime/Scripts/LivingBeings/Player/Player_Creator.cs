using UnityEngine;

namespace HeroicFantasy.LivingBeings
{
    [System.Serializable]
    public struct Player
    {
        [Header("INFOS")]
        public string name;
        public int level;

        [Header("HEALTH")]
        public int currentHP;
        public int maxHP;

        [Header("MANA")]
        public int currentMP;
        public int maxMP;

        [Header("XP")]
        public int currentXP;
        public int maxXP;
        public int totalXP;


        // Player CTOR
        public Player
        (
            // PARAMETERS

            // Infos
            string pName,
            int pLevel,

            // Health
            int pCurrentHP,
            int pMaxHP,

            // Mana
            int pCurrentMP,
            int pMaxMP,
            
            // XP
            int pCurrentXP,
            int pMaxXP,
            int pTotalXP
        )
        {
            // INITIALIZATION
            
            // Infos
            name = pName;
            level = pLevel;

            // Health
            currentHP = pCurrentHP;
            maxHP = pMaxHP;

            // Mana
            currentMP = pCurrentMP;
            maxMP = pMaxMP;
            
            // XP
            currentXP = pCurrentXP;
            maxXP = pMaxXP;
            totalXP = pTotalXP;
        }
    }
}