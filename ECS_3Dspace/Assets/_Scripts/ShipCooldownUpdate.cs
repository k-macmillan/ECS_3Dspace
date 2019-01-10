using Unity.Entities;
using UnityEngine;

namespace Ships
{

    public class ShipCooldownUpdate : ComponentSystem
    {
        public struct WeaponCooldown
        {
            public readonly int Length;
            public ComponentDataArray<Weapon> Cooldown;
        }

        [Inject] private WeaponCooldown m_WeaponCooldowns;


        protected override void OnUpdate()
        {
            SubShipWeaponCooldown();
        }



        /// <summary>
        /// Update all weapon cooldowns
        /// </summary>
        private void SubShipWeaponCooldown()
        {
            float dt = Time.deltaTime;
            for (int i = 0; i < m_WeaponCooldowns.Length; ++i)
            {
                m_WeaponCooldowns.Cooldown[i] = new Weapon { Value = m_WeaponCooldowns.Cooldown[i].Value - dt };
            }
        }
    }
}