using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera rootCamera;
    [SerializeField] public CinemachineVirtualCamera treeCamera;

    [Header("Sun Energy")]
    [SerializeField] private int maxSunEnergy = 100;
    [SerializeField] public int currentSunEnergy = 30;
    [SerializeField] private TextMeshProUGUI sunEnergyCounterText;

    [Header("Water Resource")]
    [SerializeField] private int maxWaterResource = 100;
    [SerializeField] public int currentWaterResource = 30;
    [SerializeField] private TextMeshProUGUI waterEnergyCounterText;

    [Header("Tree Vitality")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] public float currentHealth = 30;

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
        //Робимо основну камеру та що слідкує за деревом
        treeCamera.enabled = true;
        rootCamera.enabled = false;

        //Ініціалізуємо Здоров'я та ресурси
        currentHealth = maxHealth;
        currentSunEnergy = 50;
        currentWaterResource = 50;
    }

    private void Update()
    {
        HandleNextTreeStage();
        UpdateResourcesUI();
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            treeCamera.enabled = !treeCamera.enabled;
            rootCamera.enabled = !rootCamera.enabled;
        }
    }

    private void HandleNextTreeStage()
    {
        if (currentSunEnergy >= maxSunEnergy - 30 && currentWaterResource >= maxWaterResource - 30)
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

    public void AddResources(int sunEnergy, int waterResource)
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

    public void IncreaseMaxResources(int sunEnergyToAdd, int waterResourceToAdd)
    {
        maxSunEnergy += sunEnergyToAdd;
        maxWaterResource += waterResourceToAdd;
    }

    public void UpdateResourcesUI()
    {
        sunEnergyCounterText.text = currentSunEnergy.ToString();
        waterEnergyCounterText.text = currentWaterResource.ToString();
    }
}
