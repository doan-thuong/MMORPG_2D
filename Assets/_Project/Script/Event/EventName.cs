public static class EventName
{
    public static class Camera
    {
        public const string SETUP = "SETUP";
    }
    public static class Enemy
    {
        public const string ENEMY_DIE = "ENEMY_DIE";
        public const string ENEMY_SPAWN = "ENEMY_SPAWN";
        public const string ENEMY_PROVOCATIVE = "ENEMY_PROVOCATIVE";
        public const string ENEMY_NEAREST = "ENEMY_NEAREST";
    }

    public static class Hero
    {
        public const string HERO_ATTACK = "HERO_ATTACK";
        public const string SET_POSITION = "SET_POSITION";
    }

    public static class Skill
    {
        public const string USE_SKILL = "USE_SKILL";
        public const string CHOOSE_SKILL = "CHOOSE_SKILL";
        public const string START_COOLDOWN = "START_COOLDOWN";
    }

    public static class Toast
    {
        public const string PUSH_TOAST = "PUSH_TOAST";
    }

    public static class Map
    {
        public static string MAP_INIT_DONE = "MAP_INIT_DONE";
    }
}