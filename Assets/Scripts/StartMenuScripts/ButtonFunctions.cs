using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    

    [SerializeField] InputField inputField2;

    [SerializeField] InputField inputField1;


    public void GoToScene(string sceneName)
    {
        (new PlayerNames()).SetNames(inputField1.text, inputField2.text);

        SceneManager.LoadScene(sceneName);


    }
    public void Quit()
    {

        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }

    
}
