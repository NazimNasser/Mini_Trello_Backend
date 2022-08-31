namespace TodoApi

{
    public class CardDto
    {
        public string Title { get; set; } = "Prepare the assay";
        public string Category { get; set; } = "Education";
        public string? DueTime { get; set; }
        public string EstimateCount { get; set; } = "1";
        public string EstimateUnit { get; set; } = "hour";
        public string? Importance { get; set; }
        public int StatusId { get; set; } = 1;
        public int UserId { get; set; } = 1;
    }

}