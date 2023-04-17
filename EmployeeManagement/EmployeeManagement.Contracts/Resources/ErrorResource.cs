namespace EmployeeManagement.Contracts.Resources
{
    public class ErrorResource
    {
        public List<string> Messages { get; private set; }

        public ErrorResource(string message)
        {
            this.Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                this.Messages.Add(message);
            }
        }
    }
}
