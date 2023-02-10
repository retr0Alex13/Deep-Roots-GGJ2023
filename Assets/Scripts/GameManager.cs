using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    [SerializeField] private float decreaseHealthRate = 10;

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
            // DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        //������ ������� ������ �� �� ����� �� �������
        treeCamera.enabled = true;
        rootCamera.enabled = false;

        //����������� ������'� �� �������
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

    //� ����� ������� ������� ���� ������'� ��� ��������� ������'�� ������
    //� ������� ��� �� ���������
    //��� ���� �� ����, ��������� ��� \-_-/
    //  TODO: ������� �������� �� ����� ����������, ���������� ������'�, ������� � �.�.
    private void DecreaseHealth()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene("Menu");
        }
        currentHealth -= decreaseHealthRate;
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
