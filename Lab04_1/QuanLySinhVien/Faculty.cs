namespace Lab04_1.QuanLySinhVien
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faculty")]
    public partial class Faculty
    {
        [StringLength(10)]
        public string FacultyID { get; set; }

        [Required]
        [StringLength(50)]
        public string FacultyName { get; set; }

        public int? TotalProfessor { get; set; }
    }
}
