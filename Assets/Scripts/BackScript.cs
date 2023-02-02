using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BackScript : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    public GameObject segmentPrefab;
    public GameObject rowPrefab;
    private Vector2 input;
    public TextMeshProUGUI tmpText;
    [SerializeField] private Slider uFill;
    public int Duration;
    private int remainingDuration;
    private Vector2 direction;
    private int flag = 0, yRotate = 0;


    private void Start()
    {
        ResetState();
        Being(Duration);
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
        int tms = int.Parse(tmpText.text);
        if (tms != 0)
        {
            tms--;
            Transform segment = Instantiate(segmentPrefab.transform);

            Vector3 xf1 = segments[segments.Count - 1].position;
            SpriteRenderer xf2 = segment.GetComponent<SpriteRenderer>();
            // if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            // {
            //     segment.position = new Vector3(0, xf1.y - 0.8f, 0);
            //     segment.Rotate(0, 0, 0);
            //     segmentPrefab.Rotate(0, 0, 0);
            // }
            // if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            // {
            //     segment.position = new Vector3(xf1.x + 0.5f, xf1.y - 0.8f, 0);
            //     segment.Rotate(0, 0, 35);
            //     segmentPrefab.Rotate(0, 0, 35);
            // }
            // else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            // {
            //     segment.position = new Vector3(xf1.x + 0.5f, xf1.y - 0.8f, 0);
            //     segment.Rotate(0, 0, -35);
            // }
            Debug.Log(xf2.flipX);
            if(xf2.flipX)
            {
                xf2.flipX = false;
                Debug.Log(1);
            }
            else
            {
                Debug.Log(2);
                xf2.flipX = true;
            }
            if (flag == 0)
            {
                segment.position = new Vector3(xf1.x, xf1.y - 0.8f, 0);
                segment.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (flag == 1)
            {
                segment.position = new Vector3(xf1.x + 0.5f, xf1.y - 0.8f, 0);
                segment.transform.localEulerAngles = new Vector3(0, 0, 35);
            }
            if (flag == 2)
            {
                segment.position = new Vector3(xf1.x - 0.5f, xf1.y - 0.8f, 0);
                segment.transform.localEulerAngles = new Vector3(0, 0, -35);
            }
            rowPrefab.transform.position = segment.position;
            segments.Add(segment);
            tmpText.text = tms.ToString();
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
            // segment.position = new Vector3(0, xf1.y - 0.8f, 0);
            // segment.Rotate(0, 0, 0);
            //rowPrefab.transform.Rotate(0, 0, 0);
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, 0);
            flag = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || flag == 1)
        {
            // segment.position = new Vector3(xf1.x + 0.5f, xf1.y - 0.8f, 0);
            // segment.Rotate(0, 0, 35);
            //rowPrefab.transform.Rotate(0, 0, 35);
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, 35);
            flag = 1;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || flag == 2)
        {
            // segment.position = new Vector3(xf1.x + 0.5f, xf1.y - 0.8f, 0);
            // segment.Rotate(0, 0, -35);
            //rowPrefab.transform.Rotate(0, 0, -35);
            rowPrefab.transform.localEulerAngles = new Vector3(0, 0, -35);
            flag = 2;
        }
    }
}
