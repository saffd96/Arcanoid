using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPickUpTable : SingletonMonoBehaviour<RandomPickUpTable>
{
    #region Variables

    [SerializeField] private List<GameObject> pickUps;

    [SerializeField] private int[] rateTable;

    [SerializeField] private int total;
    [SerializeField] private int randomNumber;

    #endregion


    #region Events

    private void OnEnable()
    {
        Block.OnDestroyedPosition += GeneratePickUp;
    }

    private void OnDisable()
    {
        Block.OnDestroyedPosition -= GeneratePickUp;
    }

    #endregion


    private void GeneratePickUp(Vector3 blockPosition)
    {
        total = 0;

        foreach (var pickUp in rateTable)
        {
            total += pickUp;
        }

        randomNumber = Random.Range(0, total);

        if (randomNumber<=rateTable[0])
        {
            return;
        }
        for (int i = 0; i < rateTable.Length; i++)
        {
            if (randomNumber <= rateTable[i])
            {
                Instantiate(pickUps[i], blockPosition, Quaternion.identity);

                return;
            }
            else
            {
                randomNumber -= rateTable[i];
            }
        }
    }
}
