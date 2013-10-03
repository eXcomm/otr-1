add credits to:
CodeInChaos
http://stackoverflow.com/users/445517/codesinchaos

Kjell Braden kb at pentabarf.de 
http://www.cypherpunks.ca/pipermail/otr-dev/2013-February/001648.html


todo: 
- implement IDispose on objects to fully clear out objects.

= Toolkit Parse doesn't support 'OTRL_MSGTYPE_V1_KEYEXCH' and 'OTRL_MSGTYPE_UNKNOWN'
-- Copyright headers (Version400.cs) for all files, investigate what is allowed in respect to original otr copyright. 

- consider replacing SHA1 with SHA256 or SHA512 (discuss with otr-dev)

You need to have cygwin installed for the test project and otr toolkit programs
(with the proper libraries)

Socialist Millionaire's Protocol - C#
 jordanborges
http://sourceforge.net/projects/smp-csharp/

Off-the-Record Messaging
http://www.cypherpunks.ca/otr/
http://www.cypherpunks.ca/otr/README-libotr-4.0.0.txt

xmpp chat protocol

xmpp c# implementation
http://www.ag-software.de/matrix-xmpp-sdk/

The Legion of the Bouncy Castle
http://www.bouncycastle.org/csharp/

SecureString (MSDN)
http://msdn.microsoft.com/en-us/library/system.security.securestring.aspx
more information: http://blogs.msdn.com/b/shawnfa/archive/2004/05/27/143254.aspx

Diffie-Hellman
http://www.mentalis.org/soft/class.qpx?id=15
http://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange

http://www.codeproject.com/Articles/24632/Shared-Key-Generation-using-Diffie-Hellman
http://www.codeproject.com/info/cpol10.aspx

DSA
http://msdn.microsoft.com/en-us/library/system.security.cryptography.dsacryptoserviceprovider(v=vs.100).aspx
The DSA key values used in a DSAKeyValue object are:

P, Q: The DSA parameters P and Q are optional, but they must either both be present or both be absent.
G: The DSA parameter G is optional.
Y: The DSA public key value Y = G^X % P is required.
J: The DSA parameter J = (P - 1) / Q is optional, and is included solely for efficiency. If P and Q are provided in the given DSAPublicKey, J will be calculated and included.
seed, pgenCounter: The DSA prime generation seed bytes and counter are optional, but they must either both be present or both be absent.


BigInteger implementation
http://www.codeproject.com/Articles/2728/C-BigInteger-Class#ModExpEval

AES / Rijndael
http://stackoverflow.com/questions/273452/using-aes-encryption-in-c-sharp
http://msdn.microsoft.com/en-us/magazine/cc164055.aspx

pidgin / otr files
C:\Users\<username>\AppData\Roaming\.purple

#00FFA766CA99B3DA00D5ED9064CF206CF202C9C566D17F82633A6E1FA1408CFAD8E9AE6C3E76A58D907EC087261B3273A5BE3A3F5DE5D4B4978EFC0F3F40E86E3A1AF9F128BAF0A8F4288B03CAE9949D2AA8CAF444C68CEB827DA27AEB9E33365DFD0260880DB9C62FE8A12470F04EA81F6A5587A24CCC216682447A3AAA0D834B#
Fingerprint: 6770217D 00DB8829 76F46813 088800B0 DF07B6D4

cleaning of pointer
http://www.codeproject.com/Articles/32125/Unmanaged-Arrays-in-C-No-Problem
http://stackoverflow.com/questions/537573/how-to-get-intptr-from-byte-in-c-sharp 

log4net
  <appSettings>
    <add key="log4net.Config" value="log4net.config" />

log4net.Config.XmlConfigurator.Configure();

----

notes:
Bytes (BYTE): 1 byte unsigned value                   => byte
Shorts (SHORT): 2 byte unsigned value, big-endian     => ushort
Ints (INT): 4 byte unsigned value, big-endian         => uint
Multi-precision integers (MPI): 4 byte unsigned len,  => uint
   big-endian len byte unsigned value, big-endian 
   (MPIs must use the minimum-length encoding; i.e.
   no leading 0x00 bytes. This is important when
   calculating public key fingerprints.) 
Opaque variable-length data (DATA): 4 byte unsigned  => uint
   len, big-endian len byte data 
Initial CTR-mode counter value (CTR): 8 bytes data   => ...
Message Authentication Code (MAC): 20 bytes MAC data => ...


