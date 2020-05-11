using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeellsAndroidInput : MonoBehaviour
{
    [SerializeField] private Button _deflectButton;
    [SerializeField] private Button _forceButton;
    
    public  void DeflectButton()
    {
        _deflectButton.onClick.Invoke();
      
    }

    public  void ForceButton()
    {
        _forceButton.onClick.Invoke();
    }




}
