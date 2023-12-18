using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public void ChangeScene(int value)
    {
		SceneManager.LoadScene(value);
	}
    
    public void OnApplicationQuit()
    {
	    Application.Quit();
    }
}
