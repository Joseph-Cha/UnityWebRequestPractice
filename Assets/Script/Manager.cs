using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager instance;
    static Manager Instance { get { Init(); return instance; } }
    private ResourceManager resourceManager = new ResourceManager();
    public static ResourceManager Resource => Instance.resourceManager;

    // Start is called before the first frame update
    void Start() => Init();
    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Manager>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<Manager>();
        }
    }
}
