# tasks
1. Program will result the 5 most common occurences of words that is retreived from Answers API from CloudCherry.
2. First program will call the loginToken API to get the authorization token.
3. the above token is passed in header of all subsequent request to the CloudCherry APIs.
4. Questions API is called after login is successful, the multiline text questions are extraced from this API response.
5. Answers API is called to get the users answer list, the question ids retrived in 4th step is compared against the question
   ids in this API response.if match found then user input is taken out and stored.
6. On completion step 5 string that contaiins all answers is taken out and applied some pattern matching to remove unwanted prepositions,b\adverbs etc.
7. Most common occurences of 5 words are extracted and printed in console as final output
