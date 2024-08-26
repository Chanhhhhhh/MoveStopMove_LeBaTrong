using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public static readonly string RUN_ANIM_STRING = "run";
    public static readonly string IDLE_ANIM_STRING = "idle";
    public static readonly string ATTACK_ANIM_STRING = "attack";
    public static readonly string DIE_ANIM_STRING = "die";

    public const float DELAY_TIME_ATTACK = 0.5f;
    public const float DELAY_TIME_DEAD = 1f;
    public const float SPEED_DEFAULT = 8f;
    public const float RANGE_ATTACK_DEFAULT = 5f;

    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_COIN = "coin";
}
