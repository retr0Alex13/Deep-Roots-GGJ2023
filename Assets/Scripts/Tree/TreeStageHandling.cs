using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStageHandling : MonoBehaviour
{
    [Header("Player Refernce")]
    [SerializeField] private string playerTreeTag = "Player";

    [SerializeField] private int currentTreeStage = 0;
    [SerializeField] private int maxSunEnergyForStage = 30;
    [SerializeField] private int maxWaterForStage = 30;

    //[SerializeField] private float limitAngleForStage = 5;
    [SerializeField] private List<GameObject> playerPrefabsList;
    [SerializeField] private GameObject crackedSeedPrefab;
    //public delegate void TreeAction();
    //public static event TreeAction OnTreeNewStage;
    private Transform playerTransform;
    //private TreeRotation treeRotation;

    private void Start()
    {
        playerTransform = transform;
        SpawnPlayerTree();
    }

    [ContextMenu("New stage")]
    public void NewTreeStage()
    {
        //Index must be smaller than list count
        if (currentTreeStage == playerPrefabsList.Count - 1)
        {
            return;
        }

        currentTreeStage++;
        GameManager.Instance.IncreaseMaxResources(maxSunEnergyForStage, maxWaterForStage);
        //if(treeRotation != null)
        //{
        //    treeRotation.LimitTreeTilting(limitAngleForStage);
        //}
        SpawnPlayerTree();
        GameManager.Instance.treeCamera.m_Lens.OrthographicSize += 1f;

    }
    private void SpawnPlayerTree()
    {
        //Delete old
        foreach (var obj in GameObject.FindGameObjectsWithTag(playerTreeTag))
        {
            Destroy(obj);
        }
        Instantiate(playerPrefabsList[currentTreeStage], playerTransform);
        if(currentTreeStage > 0 && currentTreeStage < 3)
        {
            Instantiate(crackedSeedPrefab, playerTransform);
        }
        //treeRotation = gameObject.GetComponentInChildren<TreeRotation>();
    }
}