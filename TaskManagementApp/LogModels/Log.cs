using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApp.LogModels;

[Table("Log")]
public partial class Log
{
    [Key]
    [Column("LogID")]
    public int LogId { get; set; }

    [StringLength(500)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string LogLevel { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LogTim { get; set; }
}
