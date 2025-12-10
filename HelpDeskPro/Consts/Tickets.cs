namespace HelpDeskPro.Consts
{
    public class Tickets
    {
        public enum Statuses
        {
            Open,
            InProgress,
            WaitingCustomer,
            Resolved,
            Closed,
        }
        public enum Priorities
        {
            Low,
            Normal,
            High,
            Critical,
        }
    }
}
