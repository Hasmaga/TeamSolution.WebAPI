namespace TeamSolution.ViewModel.Feedback
{
    public class CreateNewFeedbackReqDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Guid StoreId { get; set; }
    }
}
