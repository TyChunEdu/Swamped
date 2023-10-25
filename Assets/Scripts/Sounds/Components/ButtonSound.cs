using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sounds.Components
{
    public class ButtonSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private AudioClip audioClipDown;
        [SerializeField] private AudioClip audioClipUp;
        [SerializeField] private AudioSource audioSource;

        public void OnPointerDown(PointerEventData eventData)
        {
            try
            {
                audioSource.PlayOneShot(audioClipDown);
            }
            catch (Exception)
            {
                
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (audioClipUp != null)
            {
                try
                {
                    audioSource.PlayOneShot(audioClipUp);
                }
                catch (Exception)
                {
                
                }
            }
        }

        public void WasClicked()
        {
            // Do not delete this method
        }
    }
}
