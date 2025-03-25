using System;
using System.Collections.Generic;

namespace TopicManager.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Subtopic> Subtopics { get; set; } = new List<Subtopic>();
}
