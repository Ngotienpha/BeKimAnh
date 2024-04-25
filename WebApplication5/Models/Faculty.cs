 using System;
 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
    {
        public class Faculty
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            // Một số thuộc tính khác của Faculty nếu cần
            // Ví dụ: public ICollection<Article> Articles { get; set; }
        }
    }


