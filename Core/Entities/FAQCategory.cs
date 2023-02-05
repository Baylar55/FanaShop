﻿using Core.Entities.Base;

namespace Core.Entities
{
    public class FAQCategory : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
    }
}