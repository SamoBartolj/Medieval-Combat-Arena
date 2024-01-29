using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SO
{
    public class PlayerAttacker : MonoBehaviour
    {

        PlayerAnimatorManager playerAnimatorManager;

        private void Awake()
        {
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            playerAnimatorManager.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            playerAnimatorManager.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        }
    }
}

