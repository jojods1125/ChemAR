using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public void ToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
