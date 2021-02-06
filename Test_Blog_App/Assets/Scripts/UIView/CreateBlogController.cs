using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using NativeGalleryNamespace;

namespace BlogApp
{
    public class CreateBlogController : MonoBehaviour
    {
        public RawImage m_rawImage;

        string m_title;

        string m_content;

        Texture2D m_ImageTexture;
        byte[] bytes;

        void OnEnable()
        {
            AppController.Instance.m_apiHandler.GetNewBlogData += HandleNewBlog;
        }
        public void HandleCreateBlog()
        {
            AppController.Instance.m_apiHandler.CreateBlog(bytes,m_title,m_content);
        }

        void HandleNewBlog(BlogsData blog = null)
        {
            if(blog.image!=null)
            {
                AppController.Instance.m_viewController.m_homeScreenController.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        
        public void HandleDiscardBlog()
        {
            AppController.Instance.m_viewController.m_homeScreenController.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }


        public void PickImage( int maxSize = 480 )
        {
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
            {
                Debug.Log( "Image path: " + path );
                if( path != null )
                {
                    // Create Texture from selected image
                    Texture2D texture = NativeGallery.LoadImageAtPath( path, maxSize );
                    if( texture == null )
                    {
                        Debug.Log( "Couldn't load texture from " + path );
                        return;
                    }

                    var tex = new Texture2D( 480,480, TextureFormat.RGB24, false );
                    tex = duplicateTexture(texture);
                    m_ImageTexture = tex;  
                    bytes = tex.EncodeToPNG();
                    UpDatepLaceholderImage();
                }
            }, "Select a PNG image", "image/png" );

            Debug.Log( "Permission result: " + permission );
        }

        Texture2D duplicateTexture(Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);
        
            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }
        
        public void SetTitle(string title)
        {
            m_title = title;
        }

        public void SetContent(string content)
        {
            m_content = content;
        }

        public void UpDatepLaceholderImage()
        {
            m_rawImage.texture = m_ImageTexture;
        }

        public string GetTitle()
        {
            return m_title;
        }

        public string GetContent()
        {
            return m_content;
        }
    }
}

