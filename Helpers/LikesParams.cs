﻿using System;

namespace APITON.Helpers;

public class LikesParams : PaginationParams
{
    public int UserId { get; set; }
    public string Predicate { get; set; } = "liked";
}