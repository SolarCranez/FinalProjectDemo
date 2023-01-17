using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform CharacterPosition;
    private int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator LoadNextLevel()
    {
        currentLevel++;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(currentLevel);
    }

    public void ActivateLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
