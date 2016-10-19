using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAToolKit.Models
{
    class Processes
    {
        public int Spid { get; set; }
        public int BlkBy { get; set; }
        public int ElapsedMS { get; set; }
        public int CPU { get; set; }
        public int IOReads { get; set; }
        public int IOWrites { get; set; }
        public int Executions { get; set; }
        public string CommandType { get; set; }
        public string LastWaitType { get; set; }
        public string ObjectName { get; set; }
        public string Status { get; set; }
        public string Login { get; set; }
        public string Host { get; set; }
        public string DBName { get; set; }
        public DateTime StartTime { get; set; }
        public string Protocol { get; set; }
        public string TransactionIsolation { get; set; }
        public int ConnectionWrites { get; set; }
        public int ConnectionReads { get; set; }
        public string ClientAddress { get; set; }
        public string Authentication { get; set; }
    }
}
