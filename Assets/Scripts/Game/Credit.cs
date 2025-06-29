using System.Collections;
using UnityEngine;

public class Credit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(coroutineQuitGame(3));
    }
    IEnumerator coroutineQuitGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
