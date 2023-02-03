using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Water : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    private BackScript tmpObject;
    private BoxCollider2D m_ObjectCollider;
    public int waterValue = 5;
    // Start is called before the first frame update
    void Start()
    {
        tmpText = GameObject.FindGameObjectWithTag("Sunbeam").GetComponent<TextMeshProUGUI>();
        tmpObject = GameObject.FindGameObjectWithTag("Start").GetComponent<BackScript>();
        m_ObjectCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Roots")
        {
            m_ObjectCollider.isTrigger = false;
            int tms = int.Parse(tmpText.text);
            tmpText.text = (tms + waterValue).ToString();
            this.waterValue = 0;
            tmpObject.st = false;
            tmpObject.xf1 = this.transform.position;
        }    
    }
}
