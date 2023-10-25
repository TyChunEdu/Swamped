using System;
using UnityEngine;
using UnityEngine.UI;

public class RigidScrollbar : MonoBehaviour
{
    public VerticalLayoutGroup content;
    public int visibleSlots;

    private Scrollbar _scrollbar;
    
    // Start is called before the first frame update
    void Start()
    {
        _scrollbar = gameObject.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        _scrollbar.numberOfSteps = Math.Max(0, 1 + content.transform.childCount - visibleSlots);
    }
}
