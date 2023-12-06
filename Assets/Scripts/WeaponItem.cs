using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SO
{
    [CreateAssetMenu(menuName = "Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Heavy_Attack_1;
    }
}

