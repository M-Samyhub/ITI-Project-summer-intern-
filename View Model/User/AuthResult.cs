namespace ItiFinalProject.View_Model.User
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
