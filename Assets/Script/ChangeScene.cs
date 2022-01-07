using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Button button = null;

    [SerializeField] private string nextScene = "Main Menu";

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(nextScene);
        });
    }
}
