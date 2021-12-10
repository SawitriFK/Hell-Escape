using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private string nextScene;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(nextScene);
        });
    }

}
