using System;

namespace ExamCalculator.Data
{
    public class PupilGroup
    {
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public Guid PupilId { get; set; }
        public Pupil Pupil { get; set; }
    }
}