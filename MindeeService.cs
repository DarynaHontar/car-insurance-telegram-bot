namespace CarInsuranceTelegramBot
{
    /// <summary>
    /// Class responsible for processing documents using the Mindee service.
    /// </summary>
    public class MindeeService
    {
        /// <summary>
        /// Asynchronous method for processing a document.
        /// </summary>
        /// <param name="imageBytes">Byte array representing the image of the document to be processed.</param>
        /// <returns>A string containing extracted data from the document, such as name and vehicle number.</returns>
        /// <remarks>
        /// This method simulates a real call to the Mindee API and returns fake data.
        /// Instead of a real API integration, it returns a fixed text.
        /// </remarks>
        public async Task<string> ProcessDocumentAsync(byte[] imageBytes)
        {
            // Simulate a real Mindee API call
            await Task.Delay(500); // Simulated API delay

            // Fake data
            var fakeData = new
            {
                Name = "Олександр Олександрович Коваленко",
                VehicleNumber = "АА1234ВХ"
            };

            return $"Name: {fakeData.Name}\nVehicle Number: {fakeData.VehicleNumber}";
        }
    }
}

