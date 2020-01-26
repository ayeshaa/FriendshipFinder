using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendshipFinder.Models.ViewModel
{
    public class PostUser
    {
        public int PostID { get; set; }
        public Nullable<System.DateTime> PostedOn { get; set; }
        public Nullable<int> Likes { get; set; }
        public Nullable<int> Dislikes { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public Nullable<int> UserID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string video { get; set; }
    }
}