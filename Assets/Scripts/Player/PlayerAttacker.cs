using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SO
{
    public class PlayerAttacker : MonoBehaviour
    {

        PlayerAnimatorManager playerAnimatorManager;
        public AudioManager audioManager;

        private void Awake()
        {
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            playerAnimatorManager.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
            audioManager.PlayLightAttackAudio();
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            playerAnimatorManager.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        }
    }
}

