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

        // Start is called before the first frame update
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
            if(scrollView.verticalNormalizedPosition < 0 && scrollView.verticalNormalizedPosition > -0.5 )
            {
                m_updateViewport = true;
            } 
            if(m_updateViewport)
            {
                NextPage();
            }
            if(scrollView.verticalNormalizedPosition > 1.1)
            {
                AppController.Instance.m_viewController.m_homeScreenController.RefreshHomeScreen();
            } 
        }

        void StartLoader(BlogsDataResponse blogsDataResponse = null)
        {
            StartCoroutine(Loader());
        }
        IEnumerator Loader()
        {
            yield return new WaitForSeconds(6f);
            NextPage();
        }

        public void ResetPagination()
        {
            m_counter = 0;
            m_blogCards = 0;
        } 
    }
}

