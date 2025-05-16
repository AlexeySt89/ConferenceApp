using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceApp.Domain.Entities
{
    public class ParProgramComm
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Pos { get; set; } = string.Empty;
        public string Affiliation { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
