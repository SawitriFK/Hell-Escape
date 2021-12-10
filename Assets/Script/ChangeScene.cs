using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
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
