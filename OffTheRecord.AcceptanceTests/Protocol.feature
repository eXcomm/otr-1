Feature: Protocol
	In order to use Off The Record
	As a user
	I need to be able to execute the following scenarios

Scenario: Generate a public-private key pair for a user
	Given the username "Alice" 
	And the protocol "Irc"
	When I generate a public-private key pair
	#Then the result should be 120 on the screen
