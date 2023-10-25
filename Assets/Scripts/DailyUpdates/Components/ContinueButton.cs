using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DailyUpdates.Components
{
    public class ContinueButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image img;
        [SerializeField] private Sprite unpressed, pressed;
        [SerializeField] private DailyUpdates du;

        public void OnPointerDown(PointerEventData eventData)
        {
            img.sprite = pressed;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            img.sprite = unpressed;
            du.Continue();
        }

        public void WasClicked()
        {
            // Do not delete this method
        }
    }
}
