namespace HappyDay.Models
{
    public class BirthdaysModel
    {
        public BirthdaysModel()
        {
            Current = new List<BirthdayModel>();
            Coming = new List<BirthdayModel>();
        }

        public IList<BirthdayModel> Current { get; set; }
        public IList<BirthdayModel> Coming { get; set; }
    }
}
