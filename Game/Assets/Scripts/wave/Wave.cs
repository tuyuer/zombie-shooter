using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class Wave
    {
        private float waveTime;
        public float WaveTime
        {
            get { return waveTime; }
            set { waveTime = value; }
        }

        private float spawnColdTime = 0.5f;
        public float SpawnColdTime
        {
            get { return spawnColdTime; }
            set { spawnColdTime = value; }
        }

        private float waveElapsedTime;
        private float nextSpawnTime;

        private int weakWeight;
        public int WeakWeight
        {
            get { return weakWeight; }
            set { weakWeight = value; }
        }

        private int strongWeight;
        public int StrongWeight
        {
            get { return strongWeight; }
            set { strongWeight = value; }
        }

        private int tankWeight;
        public int TankWeight
        {
            get { return tankWeight; }
            set { tankWeight = value; }
        }

        public delegate void OnSpawnZombie();
        public event OnSpawnZombie onSpawnZombie;

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
            waveElapsedTime += Time.deltaTime;
            CheckSpawn();
        }

        public zombie_type GenerateZombieType()
        {
            int nTotalWeight = weakWeight + strongWeight + tankWeight;
            int nRandomWeight = Random.Range(0, nTotalWeight);
            if (nRandomWeight <= weakWeight)
            {
                return zombie_type.zombie_type_weak;
            }
            else if (nRandomWeight <= weakWeight + strongWeight)
            {
                return zombie_type.zombie_type_strong;
            }

            return zombie_type.zombie_type_tank;
        }

        private void CheckSpawn()
        {
            if (waveElapsedTime > nextSpawnTime)
            {
                nextSpawnTime += spawnColdTime;
                if (onSpawnZombie != null)
                    onSpawnZombie();
            }
        }
    }

}