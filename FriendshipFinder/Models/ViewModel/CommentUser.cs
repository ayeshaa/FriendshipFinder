using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendshipFinder.Models.ViewModel
{
    public class CommentUser
    {
        public int CommentID { get; set; }
        public string Description { get; set; }
        public Nullable<int> PostId { get; set; }
        public Nullable<int> UserId { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
    }
}