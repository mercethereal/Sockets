To take this test please following the following instructions. You should read over the entire test before
starting to make sure you have the programming abilities needed to make the test successful.
Please note, the below is an overall overview of the steps. There will be one or two things you will
have to figure out on your own to complete this test.
Test Instructions
1. Open a TCP connection to <redacted> on port 8765
2. Send a simple XML message over the socket like this:
<?xml version="1.0" encoding="ISO-8859-1"?>

<request>
<requestID>1</requestID>
</request>

3. Receive the response. The response will contain the original requestID, so you can correspond
the response to your original request. It will also contain a message, a seemingly random string,
that you will need to save for a later step. The response will look like this:
<?xml version="1.0" encoding="ISO-8859-1"?>

<response>
<responseID>1234</responseID>
<requestID>1</requestID>
<message>abc</message>
</response>

4. Do this 500 times, incrementing the requestID attribute each time. You will send in xml
messages with requestID 1 through 500.
5. You MUST send all 500 messages and receive their responses in less than 30 seconds. This is
not meant to be test of your bandwidth or processor speed; this should be easily accomplished
in less than 30 seconds on a normal system with a normal connection. It's ok if you don't get it
the first time, your time resets when you send in a message with requestID 1. If more than 30
seconds has elapsed between the first message and the last message, the responses will not
provide an accurate answer to the test.
6. Put the responses in reverse order, based on the requestID (from 500 to 1). Take the 3rd
character of the "message" attribute of each request and append it to a string. That 3rd
character may be a space or non-alphanumeric character, so don't trim the message attribute.
You will have a 500 character string which is the answer to this test. You will understand the
answer if this is done right.
