using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlogApp
{
    public class ScrollScript : MonoBehaviour
    {
        public ScrollRect scrollView;
        public GameObject scrollContent;
        int m_pageNumber;


        [SerializeField]
        HomeScreenController m_homePageController = new HomeScreenController();

        int m_blogCards = 0;

        bool m_updateViewport = false;
        int m_counter = 0;

        void Start()
        {
            AppController.Instance.m_apiHandler.GenerateBlogCards += StartLoader;
        }

        /// <summary>
        /// This method is responsible for UI side pagination
        /// </summary>
        public void NextPage()
        {
            m_blogCards += 5;
            for(; m_counter < m_blogCards; m_counter++)
            {
                m_homePageController.GenerateItem(scrollContent, this, m_counter);
            }
            m_updateViewport = false;
        }


        // Update is called once per frame
        void Update()
        {
            // Check for page end to generate more content
            if(scrollView.verticalNormalizedPosition < 0 && scrollView.verticalNormalizedPosition > -0.5 )
            {
                m_updateViewport = true;
            } 
            if(m_updateViewport)
            {
                NextPage();
            }

            // check for pull-to-refresh
            if(scrollView.verticalNormalizedPosition > 1.1)
            {
                AppController.Instance.m_viewController.m_homeScreenController.RefreshHomeScreen();
            } 
        }

        /// <summary>
        /// This method is responsible for initiating the loader
        /// </summary>
        void StartLoader(BlogsDataResponse blogsDataResponse = null)
        {
            StartCoroutine(Loader());
        }

        /// <summary>
        /// This method is responsible for wait before loader is disable
        /// </summary>
        IEnumerator Loader()
        {
            yield return new WaitForSeconds(6f);
            NextPage();
        }

        /// <summary>
        /// This method is responsible for resetting pagination
        /// </summary>
        public void ResetPagination()
        {
            m_counter = 0;
            m_blogCards = 0;
        } 
    }
}

