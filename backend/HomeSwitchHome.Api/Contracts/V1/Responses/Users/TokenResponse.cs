namespace HomeSwitchHome.API.Contracts.V1.Responses.Users
{
    public class TokenResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }

        public string user_name { get; set; }
        public string user_role { get; set; }
        public int user_id { get; set; }
    }
}