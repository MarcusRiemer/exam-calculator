using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamCalculator.Data
{
    public record ExaminationPupilResult(Pupil Pupil, Examination Examination)
    {
        public IEnumerable<ExaminationTaskResult> TaskResults =>
            Examination.TaskResults.Where(res => res.PupilId == Pupil.PupilId);

        public bool IsComplete => TaskResults.All(res => res.Score.HasValue);

        public int OverallPoints => TaskResults.Sum(res => res.Score ?? 0);
        
        public float OverallPercentage
        {
            get
            {
                var results = TaskResults.ToArray();

                if (results.Length > 0)
                {
                    return results.Sum(res => res.Percent) / results.Length;
                }
                else
                {
                    return 0;
                }
            }
        }
        
        
    };
}