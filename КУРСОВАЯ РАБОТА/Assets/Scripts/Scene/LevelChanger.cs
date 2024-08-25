using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelChanger : MonoBehaviour
{
    private Animator _animator;
    public int levelToLoad;
    public Vector3 position;
    public VectorValue playerStorage;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    public void FadeToLevel()
    {
        _animator.SetTrigger("fade");
    }

    public void OnFadeComplete()
    {
        playerStorage.initialValue = position;
        SceneManager.LoadScene(levelToLoad);
    }
}
