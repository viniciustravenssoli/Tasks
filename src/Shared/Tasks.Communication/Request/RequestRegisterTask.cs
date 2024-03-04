using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Communication.Enums;

namespace Tasks.Communication.Request;
public class RequestRegisterTask
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Enums.TaskStatus Status { get; set; }
}
