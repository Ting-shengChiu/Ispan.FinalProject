﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace webapi.Models
{
    public partial class TrendingQuestionType
    {
        public TrendingQuestionType()
        {
            TrendingQuestions = new HashSet<TrendingQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TrendingQuestion> TrendingQuestions { get; set; }
    }
}