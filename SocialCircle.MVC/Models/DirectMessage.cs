namespace SocialCircle.MVC.Models
{
    public class DirectMessage
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string MessageText { get; set; } = "";

        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}