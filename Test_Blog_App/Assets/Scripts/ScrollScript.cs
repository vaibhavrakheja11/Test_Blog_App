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

        public GameObject scrollItemPrefab;

        public int totalBoxes;

        public bool UpdateViewport = false;

        void Start()
        {
            NextPage();
        }

        // Start is called before the first frame update
        void NextPage()
        {
            totalBoxes += 10;
            for(int a = 0; a <= totalBoxes; a++)
            {
                GenerateItem();
            }
            scrollView.verticalNormalizedPosition = 1;
            UpdateViewport = false;
            
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(scrollView.verticalNormalizedPosition);
            if(scrollView.verticalNormalizedPosition < 0)
            {
                UpdateViewport = true;
            } 
            if(UpdateViewport)
            {
                NextPage();
            }
        }

        void GenerateItem()
        {
            GameObject scrollItemObj = Instantiate(scrollItemPrefab);
            scrollItemObj.transform.SetParent(scrollContent.transform, false);
        }
    }
}

