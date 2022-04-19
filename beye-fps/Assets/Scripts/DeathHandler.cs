using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.enabled = false;
    }
    public void ProcessDeath()
    {
        gameOverUI.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
