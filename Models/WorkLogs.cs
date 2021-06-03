using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notes.Models
{
    public class WorkLogs
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public int DealWithId { get; set; }
        public string DealWithName { get; set; }
        public string DealWith { get; set; }
        public int RemarkId { get; set; }
        public string Remark { get; set; }
        public bool IsDeleted { get; set; }
    }
}