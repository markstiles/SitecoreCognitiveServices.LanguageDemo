using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Services.Models
{
    public class TrainingResult
    {
        public TrainingResult()
        {
            Confidence = 0;
            Clicks = 0;
        }

        public double Confidence { get; set; }
        public int Clicks { get; set; }
        public string RewardActionId { get; set; }
        public bool Rewarded { get; set; }

        /*return training values 2000 clicks = 10% confidence, 3000 clicks = 18% confidence */
    }
}