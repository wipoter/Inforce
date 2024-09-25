﻿using BackEnd.Models;

namespace BackEnd.Services;

public interface IUserService
{
    Task<User> GetUser(string login);
}