using UnityEngine;

namespace Ships
{
    public class Settings
    {
        public const float MaxShipSpeed = 100f;
        public const float MouseLookSensitivity = 1.0f;
        public const float ProjectileDepletedUraniumSpeed = 300.0f;
        public const float ProjectileRadius = 0.5f;
        public const float ProjectileCooldown = 0.3f;
        public const float ProjectileDamage = 30f;
        public const float ProjectileMaxDistance = 9000000f;

        public const float SubShipRadius = 6.5f;
    }

    public static class PlayerSettings
    {
        public static float MasterVolume = 1.0f;
        public static float MusicVolume = 1.0f;
        public static float SFXVolume = 1.0f;

        public static int ScreenWidth = 2560;
        public static int ScreenHeight = 1440;
        public static int DesiredFPS = 144;
        public static FullScreenMode ScreenMode = FullScreenMode.FullScreenWindow;
    }
}

namespace CommanderShips
{
    public class Settings
    {
        public const float ProjectileCooldownModifier = 0f;
        public const float ProjectileDamageModifier = 0f;
    }
}