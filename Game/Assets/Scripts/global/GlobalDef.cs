﻿using System.Collections;
using System.Collections.Generic;

public class GlobalDef
{
    //Common Funcs
    public const bool ENABLE_STICKJOY = false;

    //Common Defines
    public const int INVALID_VALUE = -1;
    public const float ZERO_VALUE = 0.0f;

    //3D World
    public const float WORLD_GRAVITY = 9.8f;

    //Actor Defines
    public const float ACTOR_MAX_FOWARD_SPEED = 1.0f;
    public const float ACTOR_MOVE_SPEED = 6f;
    public const float ACTOR_VAULT_SPEED = 4f;
    public const float ACTOR_DODGE_SPEED = 6f;
    public const float ACTOR_JUMP_SPEED = 24f;
    public const float ACTOR_JUMP_SPEED_ACCEL = 18f;
    public const float ACTOR_SWORD_ATTACKUP_SPEED = 16.2f;
    public const float ACTOR_FOWARD_WALK_SPEED = 0.3f;
    public const float ACTOR_QUICK_TURN_SPEED = ACTOR_MAX_FOWARD_SPEED / 2;

}
