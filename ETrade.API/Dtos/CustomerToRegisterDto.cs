namespace ETrade.API.Dtos
{
    public class CustomerToRegisterDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Payment { get; set; }

        public string? Email { get; set; }
    }
}
