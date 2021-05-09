using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;
    [SerializeField] private SimpleMenuController menuControls;

    private void Start()
    {
        menuControls.OnProgressChanged += MenuControls_OnProgressChanged;
    }

    private void MenuControls_OnProgressChanged(float obj)
    {
        loadingBar.value = obj;
    }
}
