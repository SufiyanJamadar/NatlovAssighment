namespace NatlovAssighment.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public decimal Score { get; set; }
        public string Comments { get; set; }
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }
    }
}
