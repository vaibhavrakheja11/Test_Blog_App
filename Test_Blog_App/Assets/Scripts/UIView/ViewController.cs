using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlogApp
{
    public class ViewController : MonoBehaviour
    {
        public LoginController m_loginController;
        public HomeScreenController m_homeScreenController;
        public BlogDetailsController m_blogScreenController;
        public CreateBlogController m_createBlogController;
        public ErrorUIController m_errorUIController;
    }
}

