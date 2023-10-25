using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class IgnoreBackgroundOnImageClick : MonoBehaviour
    {
        public Image image;
    
        // Start is called before the first frame update
        void Start()
        {
            image.alphaHitTestMinimumThreshold = .4f;
        }
    }
}
