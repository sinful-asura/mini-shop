namespace Models {
    public class Login {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    
    public enum Department {
        None,
        Owner,
        Sales,
        IT,
        Marketing,
        Other
    }
}