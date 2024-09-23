using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public static readonly string RUN_ANIM_STRING = "run";
    public static readonly string IDLE_ANIM_STRING = "idle";
    public static readonly string ATTACK_ANIM_STRING = "attack";
    public static readonly string DIE_ANIM_STRING = "die";
    public static readonly string DANCE_ANIM_STRING = "dance";
    public static readonly string WIN_ANIM_STRING = "win";
    public static readonly string ULTI_ANIM_STRING = "ulti";

    public const float DEFAULT_SIZE_CAMERA = 12f;
    public const float DELAY_TIME_ATTACK = 0.45f;
    public const float DELAY_TIME_DEAD = 1f;
    public const float TIME_COOLDOWN = 1f;
    public const float SPEED_DEFAULT = 8f;
    public const float RANGE_ATTACK_DEFAULT = 5f;
    public const float SPEED_BULLET = 10f;

    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_OBSTACLE = "Obstacle";

    public const string STRING_EQUIPED = "EQUIPED";
    public const string STRING_SELECT = "SELECT";

    public static List<string> names = new List<string>
       { 
            "John",
            "Emma",
            "Liam",
            "Olivia",
            "Noah",
            "Ava",
            "Lucas",
            "Mia",
            "Ben",
            "Sophia",
            "James",
            "Charlotte",
            "Elijah",
            "Emily",
            "Henry",
            "Grace",
            "David",
            "Lily",
            "Leo",
            "Anna",
        };
    public static string GetName()
    {
        return names[Random.Range(0, names.Count - 1)];
    }

}
