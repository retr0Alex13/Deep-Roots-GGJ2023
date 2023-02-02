using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Sun Energy")]
    [SerializeField] private float maxSunEnergy = 100;
    [SerializeField] private float currentSunEnergy = 30;

    [Header("Water Resource")]
    [SerializeField] private float maxWaterResource = 100;
    [SerializeField] private float currentWaterResource = 30;

    [Header("Tree Vitality")]
    [SerializeField] private float maxVitality = 100;
    [SerializeField] private float currentVitality = 30;

    [SerializeField] TreeStageHandling treeStageHandler; 

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(GameManager).Name;
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(currentSunEnergy >= maxSunEnergy && currentWaterResource >= maxWaterResource)
        {
            treeStageHandler.NewTreeStage();
        }
    }

    public void AddResources(float sunEnergy, float waterResource)
    {
        currentSunEnergy += sunEnergy;
        currentWaterResource += waterResource;

        Mathf.Clamp(sunEnergy, 0, maxSunEnergy);
        Mathf.Clamp(waterResource, 0, maxWaterResource);
    }
    public void IncreaseMaxResources(float sunEnergy, float waterResource)
    {
        maxSunEnergy += sunEnergy;
        maxWaterResource += waterResource;
    }
}
