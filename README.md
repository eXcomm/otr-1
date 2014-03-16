Off-the-Record Messaging (.NET)
==============

Project Description
--------------

This is an attempt to implement the Off-the-Record Messaging protocol in .NET

Currently, the following tools are implemented:

- otr_parse
- otr_sesskeys
- otr_mackey

Current shortcomings
--------------

**! Please read the following section carefully**

This project is under active development and not yet ready for release.

Current limitations/shortcomings:
- This project is under active development and not yet ready for release and therefore not yet ready or not sufficiently tested to use for secure chat.
- Strings stored within memory are not cleared/wiped after use and are therefore vulnerable for a (cross) process memory attack.
- many more...!!

From the original Off-the-Record Messaging website:

**What is Off-the-Record (OTR) Messaging**

Off-the-Record (OTR) Messaging allows you to have private conversations over instant messaging by providing:

Encryption ** No one else can read your instant messages.
Authentication ** You are assured the correspondent is who you think it is.
Deniability ** The messages you send do not have digital signatures that are checkable by a third party. Anyone can forge messages after a conversation to make them look like they came from you. However, during a conversation, your correspondent is assured the messages he sees are authentic and unmodified.
Perfect forward secrecy ** If you lose control of your private keys, no previous conversation is compromised.

Original website: http://www.cypherpunks.ca/otr/