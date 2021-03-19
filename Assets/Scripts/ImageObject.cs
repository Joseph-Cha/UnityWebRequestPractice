using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{
    private Image _imageView;

    public ImageObject(Sprite texture)
    {
        
        _imageView = gameObject.AddComponent<Image>();
        _imageView.sprite = texture;        
    }

    private void Update()
    {
        //transform.Translate()
    }
}
