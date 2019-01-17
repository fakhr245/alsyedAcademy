namespace alsyedAcademy.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int tId { get; set; } //tutotrial Id
        
        public int uId { get; set; } // user id
        public string text { get; set; }
        public string dated { get; set; }
    }
}