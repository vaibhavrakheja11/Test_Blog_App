using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace BlogApp
{
    public class SpawnItem : MonoBehaviour
    {
        public GameObject Spinner;
        public RawImage rawImage;

        [HideInInspector]
        public BlogsData m_blogData; 

        [SerializeField] 
        TextMeshProUGUI m_titleText;

        [SerializeField] 
        TextMeshProUGUI m_authorText;


        [SerializeField] 
        TextMeshProUGUI m_dateText;

        /// <summary>
        /// This method is responsible to handle Blog Details Button
        /// </summary>
        public void HandleBlogDetailsEvent()
        {
            AppController.Instance.m_viewController.m_homeScreenController.ShowDetails(m_blogData, rawImage.texture);
        }

        void Start()
        {
            m_titleText.text = m_blogData.title;
            m_authorText.text = m_blogData.author_user.name;
            m_dateText.text = m_blogData.created_at.Split(' ').First();
        }
    }
}

