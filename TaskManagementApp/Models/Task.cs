using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApp.Models;

public partial class Task
{
    [Key]
    [Column("TaskID")]
    public int TaskId { get; set; }

    [StringLength(500)]
    public string TaskDescription { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? ExpectedClosureDate { get; set; }

    [StringLength(100)]
    public string? AssignedTo { get; set; }

    [StringLength(50)]
    public string? CompletionStatus { get; set; }
}
