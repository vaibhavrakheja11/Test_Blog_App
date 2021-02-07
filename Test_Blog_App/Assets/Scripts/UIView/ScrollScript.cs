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


        // TODO : Make it event Action script

        [SerializeField]
        HomeScreenController m_homePageController = new HomeScreenController();

        public int BlogCards = 0;

        public bool UpdateViewport = false;
        int m_counter = 0;

        void Start()
        {
            AppController.Instance.m_apiHandler.GenerateBlogCards += StartLoader;
        }

        // Start is called before the first frame update
        public void NextPage()
        {
            BlogCards += 5;
            for(; m_counter < BlogCards; m_counter++)
            {
                m_homePageController.GenerateItem(scrollContent, this, m_counter);
            }
            UpdateViewport = false;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(scrollView.verticalNormalizedPosition);
            if(scrollView.verticalNormalizedPosition < 0 && scrollView.verticalNormalizedPosition > -0.5 )
            {
                UpdateViewport = true;
            } 
            if(UpdateViewport)
            {
                NextPage();
            }

            // if(scrollView.verticalNormalizedPosition > 1.1 && scrollView.verticalNormalizedPosition < 1.3 )
            // {
            //     AppController.Instance.m_viewController.m_homeScreenController.ReturnToHomeScreen();
            // } 
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

        
    }
}

