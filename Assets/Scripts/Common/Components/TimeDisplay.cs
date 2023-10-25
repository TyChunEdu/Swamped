using Singletons;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    private TMP_Text _text;
    
    // Start is called before the first frame update
    void Start()
    {
        _text = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = GameTime.Instance.TimeString();
        _text.color = GameTime.Instance.paused ? Color.red : Color.white;
    }
}
