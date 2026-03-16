namespace ProductCatalog
{
    public enum UserRole { Client, Admin }
    public class User
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}