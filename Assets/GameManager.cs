using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputSettings))]
public class GameManager : MonoBehaviour //GameManger Singleton
{

    private static GameManager _self;

    #region SingletonItems
    private InputSettings inputSettings;
    #endregion SingletonItems

    [SerializeField]
    private bool isIngame;

    void Start()
    {
        this.inputSettings = this.gameObject.GetComponent<InputSettings>();
        _self = this;
    }

    private void Update()
    {
        if (isIngame)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            if (Cursor.lockState != CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }

    #region TryGets
    public static bool TryGetInputSettings(out InputSettings _inputSettings)
    {
        _inputSettings = null;

        if (!_self.inputSettings.IsReady) return false;

        _inputSettings = _self.inputSettings;

        return true;
    }
    #endregion TryGets
}
