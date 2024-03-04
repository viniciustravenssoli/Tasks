using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Communication.Response;
public class ResponseRegisterTask
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public Enums.TaskStatus Status { get; set; }
}
