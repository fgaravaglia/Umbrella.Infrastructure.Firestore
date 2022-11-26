using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Infrastructure.Firestore.Tests
{
    public class CredentialManager
    {
        const string PROJECTID = "umbrella-dev-369813";
        const string CREDENTIAL_FILE_PATH = @"C:\Users\francesco\OneDrive\Coding\umbrella-dev-azuredevops.json";

        public string ProjectID { get{ return PROJECTID; }}

        public string CredentialsFilePath { get { return CREDENTIAL_FILE_PATH; } }

        public void SetCredentialsForGCP()
        {
            SetCredentialsIntoEnvironmentVariables(CREDENTIAL_FILE_PATH);
        }

        static void SetCredentialsIntoEnvironmentVariables(string credentialFilePath)
        {
            if(String.IsNullOrEmpty(credentialFilePath))
                throw new ArgumentNullException(nameof(credentialFilePath));

            var path = Path.GetFullPath(credentialFilePath);
            if(!File.Exists(path))
                throw new FileNotFoundException($"Unable to find the credential json file", path);

            var variableValue = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            if (String.IsNullOrEmpty(variableValue))
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
        }
    }
}