using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    [System.Serializable]
    public class Wave
    {
        public float waveTime = 10;
        public float spawnColdTime = 0.5f;
        public int weakWeight = 50;
        public int strongWeight = 30;
        public int tankWeight = 20;

        public float WaveTime
        {
            get { return waveTime; }
            set { waveTime = value; }
        }

        public float SpawnColdTime
        {
            get { return spawnColdTime; }
            set { spawnColdTime = value; }
        }

        public int WeakWeight
        {
            get { return weakWeight; }
            set { weakWeight = value; }
        }

        public int StrongWeight
        {
            get { return strongWeight; }
            set { strongWeight = value; }
        }

        public int TankWeight
        {
            get { return tankWeight; }
            set { tankWeight = value; }
        }

        private float waveElapsedTime;
        private float nextSpawnTime;

        private float wavePrepareTime = 6;
        private float wavePrepareElapsed = 0;


        public delegate void OnSpawnZombie();
        public event OnSpawnZombie onSpawnZombie;

        public delegate void OnWavePrepareBegin(Wave wave);
        public event OnWavePrepareBegin onWavePrepareBegin;

        public delegate void OnWavePrepareUpdate(Wave wave, float prepareElapsed, float prepareTime);
        public event OnWavePrepareUpdate onWavePrepareUpdate;

        public delegate void OnWavePrepareEnd(Wave wave);
        public event OnWavePrepareEnd onWavePrepareEnd;

        public Wave()
        {
            Clear();
        }

        public void Clear()
        {
            waveTime = 0;
            waveElapsedTime = 0;
            weakWeight = 0;
            strongWeight = 0;
            tankWeight = 0;
        }

        public bool IsFinished()
        {
            return waveElapsedTime > waveTime;
        }

        public void Update()
        {
            if (waveElapsedTime == 0f)
            {
                wavePrepareElapsed += Time.deltaTime;
                waveElapsedTime += Time.deltaTime;
                if (onWavePrepareBegin != null)
                    onWavePrepareBegin(this);
                return;
            }

            if (wavePrepareElapsed < wavePrepareTime  &&
                wavePrepareElapsed + Time.deltaTime > wavePrepareTime)
            {
                wavePrepareElapsed += Time.deltaTime;
                waveElapsedTime += Time.deltaTime;
                if (onWavePrepareEnd != null)
                    onWavePrepareEnd(this);
                return;
            }

            if (wavePrepareElapsed < wavePrepareTime)
            {
                wavePrepareElapsed += Time.deltaTime;
                if (onWavePrepareUpdate != null)
                    onWavePrepareUpdate(this, wavePrepareElapsed, wavePrepareTime);
                return;
            }

            waveElapsedTime += Time.deltaTime;
            CheckSpawn();
        }

        public enemy_type GenerateZombieType()
        {
            int nTotalWeight = weakWeight + strongWeight + tankWeight;
            int nRandomWeight = Random.Range(0, nTotalWeight);
            if (nRandomWeight <= weakWeight)
            {
                return enemy_type.enemy_type_zombie_weak;
            }
            else if (nRandomWeight <= weakWeight + strongWeight)
            {
                return enemy_type.enemy_type_zombie_strong;
            }

            return enemy_type.enemy_type_zombie_tank;
        }

        private void CheckSpawn()
        {
            if (waveElapsedTime > nextSpawnTime)
            {
                nextSpawnTime = waveElapsedTime + spawnColdTime;
                if (onSpawnZombie != null)
                    onSpawnZombie();
            }
        }
    }

}