using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCreateManager : MonoBehaviour
{
    public InputField InputField = null;
    public CrawlingManager CrawlingManager;
    DataTpye DataTpye;

    private string keyword
    {
        get
        {
            return InputField.text; 
        }
    }

    public void OnSearch()
    {
        CrawlingManager.CrawlingDate(keyword, DataTpye.Image);
    }

    private void LoadSprite()
    {

    }

    private void CreateImageInstance()
    {

    }


}
