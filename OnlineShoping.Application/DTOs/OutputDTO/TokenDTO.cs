﻿using System;

namespace OnlineShoping.Application.DTOs.OutputDTO
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpirationDateTime { get; set; }
    }
}
