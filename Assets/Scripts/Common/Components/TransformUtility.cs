using UnityEngine;


/// <summary>
/// A component with methods regarding transforms of the GameObject.
/// Inspired by http://answers.unity.com/answers/1798674/view.html
/// </summary>
public class TransformUtility : MonoBehaviour
{
    public void TranslateY(float y) => gameObject.transform.Translate(0f, y, 0f);
}