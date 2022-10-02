using HitJoy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillStatistics : MonoBehaviour
{
    private int nKillCount = 0;
    private Dictionary<enemy_type, int> killStatistic = new Dictionary<enemy_type, int>();

    void Awake()
    {
        for (int i = 0; i < (int)enemy_type.enemy_type_count; i++)
        {
            killStatistic.Add((enemy_type)i, 0);
        }
    }

    public int KillCount()
    {
        int nCount = 0;
        for (int i = 0; i<(int)enemy_type.enemy_type_count; i++)
        {
            nCount += killStatistic[(enemy_type)i];
        }
        return nCount;
    }

    public void KillEnemy(GameObject enemy)
    {
        EnemyInfo enemyInfo = enemy.GetComponent<EnemyInfo>();
        if (enemyInfo != null)
        {
            killStatistic[enemyInfo.enemyType]++;

            if (enemyInfo.enemyType == enemy_type.enemy_type_zombie_tank)
            {
                if (UnityEngine.Random.Range(0,100) > 50)
                {
                    DropManager.Instance.DropElement(backpack_element_type.backpack_element_type_healpack, 1, enemy.transform.position);
                }
                else
                {
                    DropManager.Instance.DropElement(backpack_element_type.backpack_element_type_gold, 1, enemy.transform.position);
                }
            }
        }
    }
}
