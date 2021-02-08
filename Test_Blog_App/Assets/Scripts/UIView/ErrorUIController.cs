using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorUIController : MonoBehaviour
{
    [SerializeField]
    GameObject m_errorBox;

    [SerializeField]
    GameObject m_targetTransform;

    [SerializeField]
    GameObject m_originalTransform;

    [SerializeField] 
    TextMeshProUGUI m_errorText;
    
    
    /// <summary>
    /// This method is responsible to initiate error to it original position
    /// </summary>
    void Start()
    {
        LeanTween.move( m_errorBox, m_originalTransform.transform, 1f);
    }

    /// <summary>
    /// This method is responsible to show error using lean tween movemnt
    /// </summary>
    public void ShowError(string error)
    {
        LeanTween.move( m_errorBox, m_targetTransform.transform, 1f);
        m_errorText.text = error;
        StartCoroutine(HideErrorSeconds()); 
    }

    /// <summary>
    /// This method is responsible to hide error
    /// </summary>
    public void HideError()
    {
        LeanTween.move( m_errorBox, m_originalTransform.transform, 1f);
    }

    /// <summary>
    /// This method is responsible to automatically hide error after 3 seconds
    /// </summary>
    IEnumerator HideErrorSeconds()
    {
        yield return new WaitForSeconds(3f);
        HideError();
    }
}
