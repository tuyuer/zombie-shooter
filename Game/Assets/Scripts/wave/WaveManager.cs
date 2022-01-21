using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class WaveManager : MonoBehaviour
    {
        public ZombieSpawnPoint[] zombieSpawnPoints;
        public List<Wave> waves = new List<Wave>();

        public GameObject[] weakZombies;
        public GameObject[] strongZombies;
        public GameObject[] tankZombies;

        private float setupTime = 1.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (setupTime > 0)
            {
                setupTime -= Time.deltaTime;
                return;
            }

            UpdateWave();
        }

        void UpdateWave()
        {
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
                
                BuildWave(curWave);
            }
            else
            {
                BuildWave();
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
                wave.WaveTime = 30;
                wave.WeakWeight = 600;
                wave.StrongWeight = 200;
                wave.TankWeight = 100;
            }
        }

        void OnSpawnZombie()
        {
            Wave curWave = waves[waves.Count - 1];
            zombie_type genType = curWave.GenerateZombieType();
            switch (genType)
            {
                case zombie_type.zombie_type_weak:
                    SpawnWeakZombies();
                    break;
                case zombie_type.zombie_type_strong:
                    SpawnStrongZombies();
                    break;
                case zombie_type.zombie_type_tank:
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

        public void GetTotalCount()
        {

        }
    }
}

