﻿using System;
using System.Collections.Generic;

namespace dal.models;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public string? UserId { get; set; }

    public string Activity { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public int? UserId1 { get; set; }

    public virtual User? UserId1Navigation { get; set; }
}
