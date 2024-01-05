using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class EmailTemplateDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ToUser { get; set; }
        public string? Subject { get; set; }
        public string? Cc { get; set; }
        public string? CcTeam { get; set; }
        public string? Body { get; set; }
    }
}
