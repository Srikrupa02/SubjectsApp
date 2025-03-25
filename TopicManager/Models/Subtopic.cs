using System;
using System.Collections.Generic;

namespace TopicManager.Models;

public partial class Subtopic
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Subject Subject { get; set; } = null!;
}
