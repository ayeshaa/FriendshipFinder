using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendshipFinder.Models.ViewModel
{
    public class ContentUser
    {
        public int ContentID { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
    }
}