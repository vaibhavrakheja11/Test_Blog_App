using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlogApp
{
    public class SpawnItem : MonoBehaviour
    {
        public GameObject Spinner;
        public RawImage rawImage;
        public BlogsData m_blogData; 

        public void HandleBlogDetailsEvent()
        {
            AppController.Instance.m_viewController.m_homeScreenController.ShowDetails(m_blogData, rawImage.texture);
        }
    }
}

