namespace MSDevsHK.Website.Data.Models
{
    public class MeetupUserIdentity
    {
        public string PictureUrl { get; set; }
        public string MeetupId { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public MeetupUserIdentity MeetupIdentity { get; set; }
    }
}