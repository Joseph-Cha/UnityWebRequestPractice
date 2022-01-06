using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateItem : MonoBehaviour
{
    public Text Year;
    public Text Month;
    public Text Day;
    
    public void SetDate(string year, string month, string day)
    {
        Year.text = year;
        Month.text = month;
        Day.text = day;
    }
}