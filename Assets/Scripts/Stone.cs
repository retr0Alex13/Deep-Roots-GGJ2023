using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour
{
    private BackScript tmpObject;
    private BoxCollider2D m_ObjectCollider;
    [SerializeField] private GameObject uFill;
    private int remainingDuration;
    public int Duration;
    // Start is called before the first frame update
    void Start()
    {
        tmpObject = GameObject.FindGameObjectWithTag("Start").GetComponent<BackScript>();
        m_ObjectCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Roots")
        {
            tmpObject.stop = 1;
            m_ObjectCollider.isTrigger = false;
            uFill.SetActive(true);
            Being(Duration);
        }
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
            uFill.GetComponent<Slider>().value = Mathf.InverseLerp(0, Duration, remainingDuration);
           this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,Mathf.InverseLerp(0, Duration, remainingDuration));
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        uFill.SetActive(false);
        tmpObject.stop = 0;
        tmpObject.st = false;
        tmpObject.xf1 = this.transform.position;
    }
}
