using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HitJoy
{
    public class WaveManager : MonoBehaviour
    {
        public Light directionLight;
        public ZombieSpawnPoint[] zombieSpawnPoints;
        public List<Wave> waves = new List<Wave>();

        public GameObject[] weakZombies;
        public GameObject[] strongZombies;
        public GameObject[] tankZombies;

        //a delay time that will setup WaveManager
        private float setupTime = 1.0f;

        private wave_manager_state state = wave_manager_state.wave_manager_running;
        private day_time dayTime = day_time.day_time_night;
        private float stateTimeElapsed = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            SwitchDayTime(day_time.day_time_night);
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsSetupFinished())
            {
                return;
            }

            if (state == wave_manager_state.wave_manager_pause)
                return;

            //in night
            if (dayTime == day_time.day_time_night)
            {
                UpdateNight();
            }
            else if (dayTime == day_time.day_time_day)
            {
                UpdateDayTime();
            }
        }

        bool IsSetupFinished()
        {
            if (setupTime > 0)
            {
                setupTime -= Time.deltaTime;
                return false;
            }
            return true;
        }

        void UpdateNight()
        {
            stateTimeElapsed += Time.deltaTime;
            if (waves.Count > 0)
            {
                Wave curWave = waves[waves.Count - 1];
                if (!curWave.IsFinished())
                {
                    curWave.Update();
                    return;
                }
                else
                {
                    int nAliveCount = GetAliveCount();
                    if (nAliveCount > 0)
                        return;
                }
                SwitchDayTime(day_time.day_time_day);
            }
        }

        void UpdateDayTime()
        {
            stateTimeElapsed += Time.deltaTime;
            if (stateTimeElapsed > 10.0f)
            {
                SwitchDayTime(day_time.day_time_night);
            }
        }

        void BuildWave(Wave lastWave = null)
        {
            Wave wave = new Wave();
            wave.onSpawnZombie += OnSpawnZombie;
            wave.onWavePrepareBegin += OnWavePrepareBegin;
            wave.onWavePrepareUpdate += OnWavePrepareUpdate;
            wave.onWavePrepareEnd += OnWavePrepareEnd;
            waves.Add(wave);

            int nWaveCount = waves.Count;
            if (lastWave != null)
            {
                wave.WaveTime = lastWave.WaveTime * 1.2f;
                wave.WeakWeight = Convert.ToInt32(lastWave.WeakWeight * 1.1f);
                wave.StrongWeight = Convert.ToInt32(lastWave.StrongWeight * 1.3f);
                wave.TankWeight = Convert.ToInt32(lastWave.TankWeight * 1.2f);
            }
            else
            {
                wave.WaveTime = 15;
                wave.WeakWeight = 600;
                wave.StrongWeight = 200;
                wave.TankWeight = 100;
            }
        }

        void SwitchDayTime(day_time dayTimeType)
        {
            if (dayTime != dayTimeType)
                stateTimeElapsed = 0f;

            dayTime = dayTimeType;
            switch (dayTime)
            {
                case day_time.day_time_day:
                    {
                        GameWorld.Instance.player.SetWeapon(weapon_type.weapon_type_pistol);
                        directionLight.DOIntensity(1.0f, 6.0f);
                        Debug.Log("EnterDayTime!!!");
                    }
                    break;
                case day_time.day_time_night:
                    {
                        directionLight.DOIntensity(0f, 1.0f);

                        if (waves.Count > 0)
                        {
                            Wave curWave = waves[waves.Count - 1];
                            BuildWave(curWave);
                        }
                        else
                        {
                            BuildWave();
                        }

                    }
                    break;
                default:
                    break;
            }
        }

        void OnSpawnZombie()
        {
            Wave curWave = waves[waves.Count - 1];
            enemy_type genType = curWave.GenerateZombieType();
            switch (genType)
            {
                case enemy_type.enemy_type_zombie_weak:
                    SpawnWeakZombies();
                    break;
                case enemy_type.enemy_type_zombie_strong:
                    SpawnStrongZombies();
                    break;
                case enemy_type.enemy_type_zombie_tank:
                    SpawnTankZombies();
                    break;
                default:
                    break;
            }
        }

        void OnWavePrepareBegin(Wave wave)
        {
            MessageObject msgObj = new MessageObject();
            msgObj.nParameter = waves.Count;
            MessageCenter.PostMessageWithData(NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_BEGIN, msgObj);
        }

        void OnWavePrepareUpdate(Wave wave, float prepareElapsed, float prepareTime) 
        {
            MessageObject msgObj = new MessageObject();
            msgObj.fParameter = prepareElapsed;
            msgObj.fParameter2 = prepareTime;
            MessageCenter.PostMessageWithData(NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_UPDATE, msgObj);
        }

        void OnWavePrepareEnd(Wave wave)
        {
            MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_END);
        }

        void SpawnWeakZombies()
        {
            GameObject clonedObj = Instantiate(weakZombies[UnityEngine.Random.Range(0, weakZombies.Length - 1)]);
            SetupZombie(clonedObj);
        }

        void SpawnStrongZombies()
        {
            GameObject clonedObj = Instantiate(strongZombies[UnityEngine.Random.Range(0, strongZombies.Length - 1)]);
            SetupZombie(clonedObj);
        }

        void SpawnTankZombies()
        {
            GameObject clonedObj = Instantiate(tankZombies[UnityEngine.Random.Range(0, tankZombies.Length - 1)]);
            SetupZombie(clonedObj);
        }

        void SetupZombie(GameObject clonedObj)
        {
            ZombieSpawnPoint spawnPoint = zombieSpawnPoints[UnityEngine.Random.Range(0, zombieSpawnPoints.Length - 1)];
            clonedObj.transform.parent = spawnPoint.transform;
            clonedObj.transform.position = spawnPoint.transform.position;
        }

        public int GetAliveCount()
        {
            int nAliveCount = 0;
            foreach (var spawnPoint in zombieSpawnPoints)
            {
                AiController[] childrenComps = spawnPoint.GetComponentsInChildren<AiController>();
                foreach (var aiController in childrenComps)
                {
                    if (!aiController.IsDeath())
                    {
                        nAliveCount++;
                    }
                }
            }
            return nAliveCount;
        }

        public void PauseWave()
        {
            state = wave_manager_state.wave_manager_pause;
        }

        public void ResumeWave()
        {
            state = wave_manager_state.wave_manager_running;
        }
    }
}

