using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BlogApp
{
    public class BlogDetailsController : MonoBehaviour
    {

        public int blogId;

        // Reference of TMP text to display author
        [SerializeField] 
        TextMeshProUGUI m_authorText;

        // Reference of TMP text to display title
        [SerializeField] 
        TextMeshProUGUI m_titleText;

        // Reference of TMP text to display views
        [SerializeField] 
        TextMeshProUGUI m_viewsText;

        // Reference of TMP text to display contents
        [SerializeField] 
        TextMeshProUGUI m_contentsText;

        // Reference of Raw Image to display image
        [SerializeField] 
        RawImage m_rawImage;


        /// <summary>
        /// This method is responsible to Show Details of the blog
        /// </summary>
        public void ShowDetails(BlogsData blogData, Texture texture)
        {
            // Set details of the blog from the data returned from the web service
            blogId = blogData.id;
            m_rawImage.texture = texture;
            m_titleText.text = blogData.title.ToString();
            m_viewsText.text = blogData.views.ToString();
            m_authorText.text = blogData.author_user.name.ToString();
            m_contentsText.text = blogData.content.ToString();
            
            // Update Image to high quality image if the blog has a valid image in response
            if(blogData.image != null)
            {
                StartCoroutine(UpdateImage(blogData.image.file_sizes.original.url));
            }
                
        }

        /// <summary>
        /// This method is responsible to close the details screen
        /// </summary>
        public void HandleCloseModal()
        {
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// This method is responsible to Sdownload high quality image and overwrite it the thumbnail image
        /// </summary>
        IEnumerator UpdateImage(string url)
        {
            WWW www = new WWW(url);
            yield return www;

            Texture2D texture = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texture);
            m_rawImage.texture = texture;
            www.Dispose();
            www = null;
        }
        

    }
}

