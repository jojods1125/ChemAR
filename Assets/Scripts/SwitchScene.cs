using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Switches the scene
/// </summary>
public class SwitchScene : MonoBehaviour
{
    /// <summary>
    /// Loads the scene specified by scene
    /// </summary>
    /// <param name="scene"> Scene name </param>
    public void ToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
