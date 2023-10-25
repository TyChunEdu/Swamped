using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ReplaceImage : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;
    private bool hover;
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerEnter;
        entry1.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry1);
        
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry2);
    }

    void Update()
    {
    }

    public void OnPointerEnterDelegate(PointerEventData data)
    {
        gameObject.GetComponent<Image>().sprite = hoverSprite;
    }
    
    public void OnPointerExitDelegate(PointerEventData data)
    {
        gameObject.GetComponent<Image>().sprite = defaultSprite;
    }

}
/*
public class ReplaceImage : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;

    
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Image>().sprite = defaultSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter()
    {
        Debug.Log("got here");
        gameObject.GetComponent<Image>().sprite = hoverSprite;
    }

    public void OnPointerExit()
    {
        gameObject.GetComponent<Image>().sprite = defaultSprite;
    }
}
*/
