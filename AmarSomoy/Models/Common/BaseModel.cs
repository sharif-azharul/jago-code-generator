using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public abstract class BaseModel
    {
        public DateTime? CreateDate { get; set; }
        [StringLength(36)]
        public string CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        [StringLength(36)]
        public string UpdateUser { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public bool IsNew { get; set; }

        //public int TotalRecord { get; set; }
        //public int? PageNo { get; set; }
        //public int? PageSize { get; set; }
        ///*– Sorting Parameters */
        //public string SortColumn { get; set; }
        //public string SortOrder { get; set; }

    }
}