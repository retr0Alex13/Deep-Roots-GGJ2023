using Unity.VisualScripting;
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
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 30;
    public float Health => currentHealth / maxHealth;

    [SerializeField] private float decreaseHealthRate = 10f;

    [Space(10)]
    [SerializeField] TreeStageHandling treeStageHandler;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentSunEnergy = 50f;
        currentWaterResource = 50f;
    }

    private void Update()
    {
        HandleNextTreeStage();
        if (currentSunEnergy < 0)
        {
            currentSunEnergy = Mathf.Clamp(currentSunEnergy, 0, maxSunEnergy);
            DecreaseHealth();
        }
        if (currentWaterResource < 0)
        {
            currentWaterResource = Mathf.Clamp(currentWaterResource, 0, maxWaterResource);
            DecreaseHealth();
        }
    }

    private void HandleNextTreeStage()
    {
        if (currentSunEnergy >= maxSunEnergy && currentWaterResource >= maxWaterResource)
        {
            treeStageHandler.NewTreeStage();
        }
    }

    //В ідеалі зробити окремий клас здоров'я для керування здоров'ям дерева
    //І зробити так із ресурсами
    //Але маємо шо маємо, обмежений час \-_-/
    //  TODO: Розбити Менеджер на окремі компоненти, Контроллер здоров'я, ресурсів і т.д.
    private void DecreaseHealth()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
            //Помер, гра закінчена
            return;
        }

        currentHealth -= Time.deltaTime / decreaseHealthRate;

    }

    public float GetCurrentWaterValue()
    {
        return currentWaterResource;
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
