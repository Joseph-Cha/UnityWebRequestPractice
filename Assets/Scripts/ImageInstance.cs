using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageInstance : MonoBehaviour
{
    public Image Image;
    private float moveSpeed = 3f;
    public ImageInstance(Sprite sprite)
    {
        Image ??= GetComponent<Image>();
        Image.sprite = sprite;
        Image?.SetNativeSize();
    }
    
    private void Start()
    {
        StartCoroutine(DestroySelf());
    }
    private void Init()
    {

    }

    private IEnumerator DestroySelf()
    {
        // 5초 후에 자동으로 파괴
        yield return null;
    }

}
