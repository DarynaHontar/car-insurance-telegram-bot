namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// This class is responsible for generating insurance policies based on the extracted user data.
    /// </summary>
    public static class PolicyGenerator
    {
        /// <summary>
        /// Generates an insurance policy based on the user session data.
        /// </summary>
        /// <param name="session">The user session that contains the extracted data (name and vehicle number).</param>
        /// <returns>A string containing the formatted insurance policy.</returns>
        /// <remarks>
        /// The policy includes:
        /// - A random policy number.
        /// - The client name and vehicle number extracted from the session.
        /// - The date the policy was created.
        /// - A fixed price of 100$.
        /// </remarks>
        public static string GeneratePolicy(UserSession session)
        {
            var extractedData = session?.ExtractedData;
            if (string.IsNullOrEmpty(extractedData))
            {
                return "Unable to generate policy. Extracted data is missing.";
            }

            // Split the extracted data into lines
            var extractedDataLines = extractedData.Split('\n');

            // Extract client name and vehicle number
            var clientName = extractedDataLines.Length > 0 ? extractedDataLines[0].Replace("Client Name: ", "") : "Not provided";
            var vehicleNumber = extractedDataLines.Length > 1 ? extractedDataLines[1].Replace("Vehicle Number: ", "") : "Not provided";

            // Create the policy template
            string policyTemplate = $@"
                Insurance Policy No. {new Random().Next(10000, 99999)}
                Client Name: {clientName}
                Vehicle Number: {vehicleNumber}
                Date of Issuance: {DateTime.UtcNow.ToShortDateString()}
                Price: 100$
                Thank you for using our services!";

            // Return the generated policy
            return policyTemplate;
        }
    }
}
