using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : Singletone<LoadSceneManager>
{
    enum SCENE_NAME
    {

    }

    private int m_iThisSceneNum;
    public int thisMenuIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        
    }
    private void OnLevelWasLoaded()
    {
        Initialized();        
    }

    private void Initialized()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneButton()
    {
        
    }
}
