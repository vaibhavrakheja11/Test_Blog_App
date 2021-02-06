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
        [SerializeField] 
        TextMeshProUGUI m_authorText;

        [SerializeField] 
        TextMeshProUGUI m_titleText;

        [SerializeField] 
        TextMeshProUGUI m_viewsText;

        [SerializeField] 
        TextMeshProUGUI m_contentsText;

        [SerializeField] 
        RawImage m_rawImage;


        public void ShowDetails(BlogsData blogData, Texture texture)
        {
            blogId = blogData.id;
            m_rawImage.texture = texture;
            m_titleText.text = blogData.title.ToString();
            m_viewsText.text = blogData.views.ToString();
            m_authorText.text = blogData.author_user.name.ToString();
            m_contentsText.text = blogData.content.ToString();
            
            if(blogData.image != null)
            {
                StartCoroutine(UpdateImage(blogData.image.file_sizes.original.url));
            }
                
        }

        public void HandleCloseModal()
        {
            this.gameObject.SetActive(false);
        }

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

