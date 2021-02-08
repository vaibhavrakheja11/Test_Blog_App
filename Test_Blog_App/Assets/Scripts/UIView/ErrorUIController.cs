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
    
    void Start()
    {
        LeanTween.move( m_errorBox, m_originalTransform.transform, 1f);
    }

    public void ShowError(string error)
    {
        LeanTween.move( m_errorBox, m_targetTransform.transform, 1f);
        m_errorText.text = error;
        StartCoroutine(HideErrorSeconds()); 
    }

    public void HideError()
    {
        LeanTween.move( m_errorBox, m_originalTransform.transform, 1f);
    }

    IEnumerator HideErrorSeconds()
    {
        yield return new WaitForSeconds(2f);
        HideError();
    }
}
