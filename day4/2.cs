using System;
using System.IO;

string[] lines = File.ReadAllLines("input.txt");

var overlappingSegments = 0;

foreach (var line in lines)
{
    var pair = line.Split(',');
    var e1_assigned_section = pair[0].Split('-');
    var e2_assigned_section = pair[1].Split('-');

    var e1_segment = new Segment(int.Parse(e1_assigned_section[0]), int.Parse(e1_assigned_section[1]));
    var e2_segment = new Segment(int.Parse(e2_assigned_section[0]), int.Parse(e2_assigned_section[1]));

    if (e1_segment.overlaps(e2_segment))
    {
        overlappingSegments += 1;
    }
}

Console.WriteLine(overlappingSegments);

public readonly struct Segment
{
    public int start { get; }
    public int end { get; }

    public Segment(int start, int end)
    {
        this.start = start;
        this.end = end;
    }

    public bool overlaps(Segment other)
    {
        bool otherSegmentStartsWithinThisSegment = other.start >= this.start && other.start <= this.end;
        bool thisSegmentStartsWithinOtherSegment = this.start >= other.start && this.start <= other.end;

        return thisSegmentStartsWithinOtherSegment || otherSegmentStartsWithinThisSegment;
    }
}