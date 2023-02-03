using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BackScript : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    private Transform segment;
    public GameObject segmentPrefab;
    public GameObject rowPrefab;
    private Vector2 input;
    public Vector3 xf1;
    public TextMeshProUGUI waterCounter;
    [SerializeField] private Slider uFill;
    public int Duration;
    private int remainingDuration;
    private Vector2 direction;
    private int flag = 0;
    public int stop = 0;
    public bool st = true;

    private void Start()
    {
        ResetState();
        Being(Duration);
        waterCounter.text = GameManager.Instance.GetCurrentWaterValue().ToString();
    }
    private void Update()
    {
        DirectinCreate();
    }

    private void Being(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uFill.value = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        GetElement();
    }
    public void GetElement()
    {
        float tms = GameManager.Instance.GetCurrentWaterValue();
        if (tms != 0)
        {
            tms -= 1;
            if (stop == 0)
            {
                segment = Instantiate(segmentPrefab.transform);
                if (st)
                {
                    xf1 = segments[segments.Count - 1].position;
                }
                if (flag == 0)
                {
                    segment.position = new Vector3(xf1.x, xf1.y - 0.6f, 0);
                    segment.transform.localEulerAngles = new Vector3(0, 0, 0);
                    if (segment.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = true;
                        segment.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = false;
                        segment.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                if (flag == 1)
                {
                    segment.position = new Vector3(xf1.x + 0.33f, xf1.y - 0.5f, 0);
                    segment.transform.localEulerAngles = new Vector3(0, 0, 35);
                    if (segment.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = true;
                        segment.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = false;
                        segment.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                if (flag == 2)
                {
                    segment.position = new Vector3(xf1.x - 0.33f, xf1.y - 0.5f, 0);
                    segment.transform.localEulerAngles = new Vector3(0, 0, -35);
                    if (segment.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = true;
                        segment.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        segmentPrefab.GetComponent<SpriteRenderer>().flipX = false;
                        segment.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                rowPrefab.transform.position = segment.position;
                segments.Add(segment);
                st = true;
            }
            waterCounter.text = tms.ToString();
            GameManager.Instance.AddResources(tms, 0);
            uFill.value = 1;
            Being(Duration);
        }
        else
        {
            Being(Duration);
        }
    }
    public void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(transform);
    }
    private void DirectinCreate()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || flag == 0)
        {
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, 0);
            flag = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || flag == 1)
        {
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, 35);
            flag = 1;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || flag == 2)
        {
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, -35);
            flag = 2;
        }
    }
    public Transform CheckPosition(Transform testing = null)
    {
        if (testing == null)
        {
            return Instantiate(segmentPrefab.transform);
        }
        else
        {
            return segment = testing;
        }
    }
}
