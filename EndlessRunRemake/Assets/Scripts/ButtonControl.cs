using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void giris(int sahne_id)
    {
        SceneManager.LoadScene(sahne_id);
    }
    public void yenidenOyna(int yeniden)
    {
        SceneManager.LoadScene(1);
    }
}
