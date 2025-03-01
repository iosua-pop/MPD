namespace SchoolApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MemberDetailsPage), typeof(MemberDetailsPage));
            Routing.RegisterRoute(nameof(ProgramariPage), typeof(ProgramariPage));
            Routing.RegisterRoute(nameof(InstrumentDetailsPage), typeof(InstrumentDetailsPage));

            Routing.RegisterRoute(nameof(FeedbackDetailsPage), typeof(FeedbackDetailsPage));
            Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));

        }
    }
}
