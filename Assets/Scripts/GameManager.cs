using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Sun Energy")]
    [SerializeField] private float maxSunEnergy = 100;
    [SerializeField] private float currentSunEnergy = 30;
    public float SunEnergy => currentSunEnergy / maxSunEnergy;

    [Header("Water Resource")]
    [SerializeField] private float maxWaterResource = 100;
    [SerializeField] private float currentWaterResource = 30;
    public float WaterResources => currentWaterResource / maxWaterResource;

    [Header("Tree Vitality")]
    [SerializeField] private float maxVitality = 100;
    [SerializeField] private float currentVitality = 30;
    public float Vitality => currentVitality / maxVitality;

    [SerializeField] private float decreaseResourceRate = 10f;

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
        HandleNextTreeStage();
        DecreaseResourcesWithTime();
    }

    private void HandleNextTreeStage()
    {
        if (currentSunEnergy >= maxSunEnergy && currentWaterResource >= maxWaterResource)
        {
            treeStageHandler.NewTreeStage();
        }
    }

    private void DecreaseResourcesWithTime()
    {
        currentSunEnergy -= Time.deltaTime / decreaseResourceRate;
        currentWaterResource -= Time.deltaTime / decreaseResourceRate;

        if (currentSunEnergy < 0)
        {
            currentSunEnergy = Mathf.Clamp(currentSunEnergy, 0, maxSunEnergy);
            DecreaseHealth();
        }
        if(currentWaterResource < 0)
        {
            currentWaterResource = Mathf.Clamp(currentWaterResource, 0, maxWaterResource);
            DecreaseHealth();
        }
    }

    //В ідеалі зробити окремий клас здоров'я для керування здоров'ям дерева
    //І зробити так із ресурсами
    //Але маємо шо маємо, обмежений час \-_-/
    //  TODO: Розбити Менеджер на окремі компоненти, Контроллер здоров'я, ресурсів і т.д.
    private void DecreaseHealth()
    {
        currentVitality -= Time.deltaTime / decreaseResourceRate;

        if (currentVitality < 0)
        {
            currentVitality = 0;
            //Помер, гра закінчена
            return;
        }
    }

    public void AddResources(float sunEnergy, float waterResource)
    {
        currentSunEnergy += sunEnergy;
        currentWaterResource += waterResource;

        if (currentSunEnergy > maxSunEnergy)
        {
            currentSunEnergy = maxSunEnergy;
        }
        if (currentWaterResource > maxWaterResource)
        {
            currentWaterResource = maxWaterResource;
        }
    }

    public void IncreaseMaxResources(float sunEnergyToAdd, float waterResourceToAdd)
    {
        maxSunEnergy += sunEnergyToAdd;
        maxWaterResource += waterResourceToAdd;
    }
}
