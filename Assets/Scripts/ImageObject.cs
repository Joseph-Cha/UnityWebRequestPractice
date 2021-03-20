using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{
    private Image _imageView = null;

    // 이미지 객체가 생성이 될 때 초기화 과정
    public ImageObject(Sprite texture = null)
    {
        if(texture == null)
        {
            Debug.Log("There is no Image downloaded on Request Site");
        }
        _imageView.sprite = texture;        
    }
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }

    // 일반 부모 오브젝트 생성
    // 해당 부모 오브젝트에 자식 오브젝트 생성
    // 자식 오브젝트에 canvas, image 붙이기
    // image에 얻어온 sprite 붙이기

    // 
}
