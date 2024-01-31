using System;

namespace APITON.DTOs;

public class CreateMessageDto
{
    public string? RecipientUsername { get; set; }
    public string? Content { get; set; }
}
