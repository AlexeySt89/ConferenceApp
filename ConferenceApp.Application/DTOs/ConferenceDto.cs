﻿namespace ConferenceApp.Application.DTOs
{
    public class ConferenceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
