using System;
using OffTheRecord.Model;
using TechTalk.SpecFlow;

namespace OffTheRecord.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ProtocolSteps
    {
        private Model.Protocol _protocol;
        private UserState _userState;

        [BeforeScenario]
        public void Initialize()
        {
            _protocol = new Model.Protocol();
            _userState = _protocol.CreateUserState();

            // won't read any existing files.
        }

        [Given(@"the username ""(.*)""")]
        public void GivenTheUsername(string username)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the protocol ""(.*)""")]
        public void GivenTheProtocol(string protocol)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I generate a public-private key pair")]
        public void WhenIGenerateAPublic_PrivateKeyPair()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
