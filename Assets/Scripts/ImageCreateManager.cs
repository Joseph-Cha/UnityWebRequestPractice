using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCreateManager : MonoBehaviour
{
    public InputField InputField;
    public DataType DataTpye;

    private string keyword => InputField.text;

    public void OnSearch()
    {
        CrawlingManager.CrawlingDate(keyword, DataTpye);
    }

    private void LoadSprite()
    {
        
    }

    private void CreateImageInstance()
    {

    }


}
