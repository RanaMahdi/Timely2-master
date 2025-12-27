namespace Timely.Dtos
{
    public class LoginRequestDto
    {
            public string Email { get; set; }
            public string Password { get; set; }
        }



        public class LoginResponse
        {
            public bool IsSuccess { get; set; }
            public string FirstName { get; set; } = default!;
            public string Email { get; set; } = default!;
        }
    }