﻿namespace Tasks.Communication.Request;
public class RequestChangePassword
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
