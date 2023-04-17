namespace EmployeeManagement.Contracts.Helpers
{
    public static class CheckExtensionHelper
    {
        public static bool CheckExtension(string ext)
        {
            var listExtension = new List<string>
            {
                ".png",
                ".jpg"
            };

            return listExtension.Contains(ext.ToLower());
        }
    }
}
