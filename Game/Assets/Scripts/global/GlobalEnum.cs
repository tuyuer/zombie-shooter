
public enum day_time
{
    day_time_day,
    day_time_night,
};

public enum wave_manager_state
{
    wave_manager_running,
    wave_manager_pause,
    wave_manager_stoped,
};

public enum enemy_type
{
    enemy_type_zombie_weak,
    enemy_type_zombie_strong,
    enemy_type_zombie_tank,
    enemy_type_zombie_boss,
    enemy_type_count,
};

public enum weapon_type
{
    weapon_type_pistol,
    weapon_type_rifle,
    weapon_type_shortgun,
    weapon_type_gun_turret,
};

public enum simple_object_pool_type
{
    simple_object_pool_type_bullet_pistol,
    simple_object_pool_type_bullet_rifle,
    simple_object_pool_type_bullet_turret,
    simple_object_pool_type_bullet_shortgun,
    simple_object_pool_type_blood,
    simple_object_pool_type_sound,
};

public enum backpack_element_type
{
    backpack_element_type_gold,
    backpack_element_type_healpack,
    backpack_element_type_count,
};