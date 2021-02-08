using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlogApp
{
    public class HomeScreenController : MonoBehaviour
    {
        
        RawImage m_displayPicture;

        public GameObject Content;
        public GameObject scrollItemPrefab;


        BlogsDataResponse m_blogsDataResponse;

        [SerializeField]
        List<GameObject> m_gameScreenLoader = new List<GameObject>();

        Queue<GameObject> m_currentTip = new Queue<GameObject>();
        
        [SerializeField]
        GameObject LoaderObject;

        [SerializeField]
        GameObject Footer;

        GameObject currentObject;
        
        // Add action associated event on enable
        public void OnEnable()
        {
            AppController.Instance.m_apiHandler.GenerateBlogCards += UpdateResponseResults;
        }

        public void Start()
        {
            // Add action associated event on start
            AppController.Instance.m_apiHandler.GenerateBlogCards += UpdateResponseResults;
            
            // Request results from web service
            AppController.Instance.m_apiHandler.HandleAllBlogsEvent();

            // Intialise loader animations into a queue
            foreach(GameObject loader in m_gameScreenLoader)
            {
                m_currentTip.Enqueue(loader);
            }

            // enable first animtion from the queue
            currentObject = m_currentTip.Peek();
            currentObject.SetActive(true);
        }

        /// <summary>
        /// This method is responsible for refreshing the homepage screen with latets content
        /// </summary>
        public void RefreshHomeScreen()
        {
            // Call web service to fetch the latest all blogs data
            AppController.Instance.m_apiHandler.HandleAllBlogsEvent();

            // Remove the [reviously generated content]
            SpawnItem[] contents = Content.gameObject.GetComponentsInChildren<SpawnItem>();
            ScrollScript scrollScript = gameObject.GetComponentInChildren<ScrollScript>();

            // Reset the pagination and scroll view
            scrollScript.ResetPagination();
            scrollScript.scrollView.verticalNormalizedPosition = 0.9f;
            
            // Destroy previously generated cards
            foreach(var con in contents)
            {
                Destroy(con.gameObject);
            }
            // Activate the loader again
            if(!LoaderObject.active)
            {
                LoaderObject.SetActive(true);
                Footer.SetActive(false);
            }
            
        }

        /// <summary>
        /// This method is responsible to save the api results and generate Cards
        /// </summary>
        public void UpdateResponseResults(BlogsDataResponse blogsDataResponse)
        {
            m_blogsDataResponse =  blogsDataResponse;
        }

        /// <summary>
        /// This coroutine is responsible to download an image thumbnail and set it to respective generated card.
        /// </summary>
         IEnumerator SetImage(string url,BlogsData blogData, SpawnItem spawnItem) 
         {
            // Get reference of image gameobject
            RawImage displayPicture = spawnItem.rawImage;

            // Set WWW object
            WWW www = new WWW(url);
            yield return www;

            // convert the response into texture
            Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texture);

            // update display picture
            displayPicture.texture = texture;

            // Incase the image has not been downloaded yet and the user opens the blog details, this will update the image in that open details modal
            if(AppController.Instance.m_viewController.m_blogScreenController.gameObject.active && blogData.id == AppController.Instance.m_viewController.m_blogScreenController.blogId)
            {
                spawnItem.HandleBlogDetailsEvent();
            }

            // Set the respective image loading animation to false
            spawnItem.Spinner.SetActive(false);

            // Set raw image object to visible
            spawnItem.rawImage.gameObject.SetActive(true);

            // Dispose the results to free memory
            www.Dispose();
            www = null;
        }

        /// <summary>
        /// This method is responsible to instantisate the blog card and set it attributes
        /// </summary>
        public void GenerateItem(GameObject scrollContent, ScrollScript scrollScript, int num)
        {   
            // Check if loader is active or not
            if(LoaderObject.active)
            {
                // if loader is active, this is the first card to be generated, hence disable the loader
                LoaderObject.SetActive(false);

                // enable the footer
                Footer.SetActive(true);
            }

            // check if the response for all blof is not empty
            if(m_blogsDataResponse.data != null)
            {
                // Generate the card and set its parent
                GameObject scrollItemObj = Instantiate(scrollItemPrefab);
                scrollItemObj.transform.SetParent(scrollContent.transform, false);
                
                // Manipulate the details of the generated card
                SpawnItem spawnItem = scrollItemObj.GetComponent<SpawnItem>();
                spawnItem.m_blogData = m_blogsDataResponse.data[num];
                
                // Try to set the image of the card from the response
                try 
                {
                    if(m_blogsDataResponse.data[num].image != null)
                    {
                        StartCoroutine(SetImage(m_blogsDataResponse.data[num].image.file_sizes.thumb.url, m_blogsDataResponse.data[num], spawnItem));
                    }
                }
                catch
                {
                    Debug.LogError("HomeScreenController.cs :: GenerateItem() :: --> Exception while trying to set Image to Blog");
                }
            }
        }

        // Remove action associated event on disable
        void OnDisable()
        {
            AppController.Instance.m_apiHandler.GenerateBlogCards -= UpdateResponseResults;
        }

        /// <summary>
        /// This method is responsible to cycle through the loading screen tip
        /// </summary>
        public void NextTip()
        {
            // disable current animation tip
            currentObject = m_currentTip.Peek();
            currentObject.SetActive(false);

            // Dequeue the current animation tip and add it back to queue
            m_currentTip.Enqueue(m_currentTip.Dequeue());

            // Display the next animation tip
            currentObject = m_currentTip.Peek();
            currentObject.SetActive(true);
        }

        /// <summary>
        /// This method is responsible to activate the Blog Details Screen Modal
        /// </summary>
        public void ShowDetails(BlogsData blogsData, Texture texture)
        {
            // Check if the modal is already open
            if(!AppController.Instance.m_viewController.m_blogScreenController.gameObject.active)
            {
                AppController.Instance.m_viewController.m_blogScreenController.gameObject.SetActive(true);
            }

            // Set the display value to one recieved fromt the server
            AppController.Instance.m_viewController.m_blogScreenController.ShowDetails(blogsData, texture);
        }

        /// <summary>
        /// This method is responsible to handle the login button on the footer of home screen
        /// </summary>
        public void HandleUserButton()
        {
            AppController.Instance.m_viewController.m_loginController.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// This method is responsible to handle the new blog button on the footer of home screen
        /// </summary>
        public void HandleNewBlogButton()
        {
            // Get the logged in users details
            ResponseData authData = AppController.Instance.m_apiHandler.GetAuthorizedData();

            // check for valid auth_token
            if(authData.token != null)
            {
                // Open login/SignUp screen in case valid details are not found
                AppController.Instance.m_viewController.m_createBlogController.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
            {
                // Open new blog screen if valid user details are found
                AppController.Instance.m_viewController.m_loginController.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        
    }
}

